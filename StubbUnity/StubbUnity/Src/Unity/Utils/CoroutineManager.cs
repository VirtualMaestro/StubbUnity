using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using UnityEngine;

namespace StubbUnity.Unity.Utils
{
    public sealed class CoroutineManager : Singleton<CoroutineManager>
    {
        /// <summary>
        /// Start classic Unity's coroutine.
        /// </summary>
        public static Coroutine Run(IEnumerator coroutine)
        {
            return I._Run(coroutine);
        }

        /// <summary>
        /// Stops classic Unity's coroutine.
        /// </summary>
        public static void Stop(Coroutine coroutine)
        {
            I._Stop(coroutine);
        }

        /// <summary>
        /// As a param uses reference to a coroutine method (not invocation result).
        /// </summary>
        public static void Run(Func<IEnumerator> func)
        {
            I._Run(func);
        }

        /// <summary>
        /// As a param uses reference to a coroutine method.
        /// </summary>
        public static void Stop(Func<IEnumerator> func)
        {
            I._Stop(func);
        }

        public static void StopAll()
        {
            I._StopAll();
        }

        /// <summary>
        /// Invokes given function every given amount of seconds.
        /// </summary>
        /// <param name="func">Function to invoke.</param>
        /// <param name="seconds">Delay between invocations in seconds.</param>
        /// <param name="numTimes">Num times invocation should happens after coroutine will be removed. -1 means infinite.</param>
        public static ICJob EverySeconds(Action func, float seconds, int numTimes = -1)
        {
            return I._RunJob(func, seconds, -1, numTimes);
        }
        
        /// <summary>
        /// Invokes given function every given amount of frames.
        /// </summary>
        /// <param name="func">Function to invoke.</param>
        /// <param name="frames">Delay between invocations in frames.</param>
        /// <param name="numTimes">Num times invocation should happens after coroutine will be removed. -1 means infinite.</param>
        public static ICJob EveryFrames(Action func, int frames, int numTimes = -1)
        {
            return I._RunJob(func, -1, frames, numTimes);
        }

        /// <summary>
        /// Invokes given function one time after a delay in a given amount of seconds. 
        /// </summary>
        public static ICJob OnceSeconds(Action func, float seconds)
        {
            return I._RunJob(func, seconds, -1, 1);
        }

        /// <summary>
        /// Invokes given function one time after a delay in a given amount of frames.. 
        /// </summary>
        public static ICJob OnceFrames(Action func, int frames)
        {
            return I._RunJob(func, -1, frames, 1);
        }

        private Dictionary<Func<IEnumerator>, Coroutine> _coroutinesMap;
        private LinkedList<CoroutineJob> _jobs;
        private Stopwatch _stopwatch;
        private long _prevTime;

        private void Awake()
        {
            _coroutinesMap = new Dictionary<Func<IEnumerator>, Coroutine>();
            _jobs = new LinkedList<CoroutineJob>();
            _stopwatch = new Stopwatch();
        }
        
        private Coroutine _Run(IEnumerator coroutine)
        {
            return StartCoroutine(coroutine);
        }
        
        private void _Stop(Coroutine coroutine)
        {
            StopCoroutine(coroutine);
        }

        private void _Run(Func<IEnumerator> func)
        {
            if (!_coroutinesMap.TryGetValue(func, out var coroutine))
            {
                coroutine = I._Run(func.Invoke());
                _coroutinesMap.Add(func, coroutine);
            }
        }
        
        private void _Stop(Func<IEnumerator> func)
        {
            if (_coroutinesMap.TryGetValue(func, out var coroutine))
            {
                I.StopCoroutine(coroutine);
                _coroutinesMap.Remove(func);
            }
        }

        private void _StopAll()
        {
            StopAllCoroutines();
            
            _stopwatch.Stop();
            _coroutinesMap.Clear();
            _DisposeAllJobs();
        }

        private ICJob _RunJob(Action func, float seconds = -1, int frames = -1, int times = -1)
        {
            var job = CoroutineJob.Get(func, seconds, frames, times);
            _jobs.AddLast(job.Node);
            
            _Run(_CoroutineLoop);

            return job;
        }

        private IEnumerator _CoroutineLoop()
        {
            _stopwatch.Start();
            _prevTime = _stopwatch.ElapsedMilliseconds;

            while (_jobs.Count > 0)
            {
                var timeStep = _stopwatch.ElapsedMilliseconds - _prevTime;
                _prevTime = _stopwatch.ElapsedMilliseconds;

                var nextNode = _jobs.First;

                while (nextNode != null && _jobs.Count > 0) // all jobs were disposed during the traversal
                {
                    var job = nextNode.Value;
                    nextNode = nextNode.Next;
                    
                    if (!job.IsPaused && job.Process(timeStep))
                        job.Dispose();
                }
                
                yield return null;
            }
        
            _Stop(_CoroutineLoop);
            _stopwatch.Stop();
        }

        private void _DisposeAllJobs()
        {
            while (_jobs.Count > 0)
                _jobs.Last.Value.Dispose();
        }

        public interface ICJob
        {
            /// <summary>
            /// Invokes when job has finished its job and disposed 
            /// </summary>
            event Action OnComplete;
            bool IsDisposed { get; }
            bool IsPaused { set; get; }
            /// <summary>
            /// Set delay in seconds.
            /// 'Times' - how many times (cycles) should a job proceed before stop.
            /// </summary>
            void SetDelay(float seconds, int times = -1);
            /// <summary>
            /// Set delay in frames.
            /// 'Times' - how many times (cycles) should a job proceed before stop. 
            /// </summary>
            void SetDelay(int frames, int times = -1);
            void Dispose();
        }
        
        private class CoroutineJob : ICJob
        {
            public event Action OnComplete;

            public readonly LinkedListNode<CoroutineJob> Node;

            private long _initMilliseconds;
            private int _initFrames;
            private long _milliseconds;
            private int _frames;
            private int _times;
            private Action _method;

            public bool IsPaused { set; get; }
            public bool IsDisposed => Node.List == null;

            public CoroutineJob(Action method, float seconds =-1, int frames = -1, int times = -1, bool isPaused = false)
            {
                Node = new LinkedListNode<CoroutineJob>(this);
                Set(method, seconds, frames, times, isPaused);
            }

            public void Set(Action method, float seconds =-1, int frames = -1, int times = -1, bool isPaused = false)
            {
                _method = method;
                
                SetDelay(seconds, times);
                SetDelay(frames, times);
                
                IsPaused = isPaused;
            }

            public void SetDelay(float seconds, int times = -1)
            {
                _milliseconds = (long)(seconds * 1000);
                _initMilliseconds = _milliseconds;
                _times = times;
            }

            public void SetDelay(int frames, int times = -1)
            {
                _frames = frames;
                _initFrames = _frames;
                _times = times;
            }

            // Returns 'true' if a job should be disposed
            public bool Process(long timeStep)
            {
                _milliseconds -= timeStep;

                if (_milliseconds > 0 || _frames-- > 0) 
                    return false;
                
                _method();
                    
                _milliseconds = _initMilliseconds;
                _frames = _initFrames;
                _times--;

                return _times == 0;
            }

            public void Dispose()
            {
                if (Node.List == null)
                    return;
                
                Node.List.Remove(Node);

                _method = null;
                
                Put(this);
                
                OnComplete?.Invoke();
            }

            // *** Pool ***//
            private static readonly Stack<CoroutineJob> Pool = new Stack<CoroutineJob>(10);

            public static CoroutineJob Get(Action method, float seconds =-1, int frames = -1, int times = -1)
            {
                if (Pool.Count == 0) 
                    return new CoroutineJob(method, seconds, frames, times);
                
                var job = Pool.Pop();
                job.Set(method, seconds, frames, times);
                return job;
            }
            
            public static void Clear()
            {
                Pool.Clear();
            }
            
            private static void Put(CoroutineJob coroutineJob)
            {
                Pool.Push(coroutineJob);
            }
        }
    }
    
    public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static readonly Lazy<T> LazyInstance = new Lazy<T>(_CreateSingleton, LazyThreadSafetyMode.PublicationOnly);

        protected static T I => LazyInstance.Value;

        private static T _CreateSingleton()
        {
            var existingInstance = FindObjectsOfType<T>();
            
            if (existingInstance == null || existingInstance.Length == 0)
            {
                var ownerObject = new GameObject($"{typeof(T).Name} (singleton)");
                var instance = ownerObject.AddComponent<T>();
                DontDestroyOnLoad(ownerObject);
                return instance;
            }

            return existingInstance[0];
        }
    }
}