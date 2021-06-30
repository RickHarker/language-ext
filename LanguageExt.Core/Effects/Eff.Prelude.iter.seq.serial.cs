using System;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using LanguageExt.Common;
using LanguageExt.Effects.Traits;
using LanguageExt.Thunks;

namespace LanguageExt
{
    public static partial class Prelude
    {
        /// <summary>
        /// Sequentially iterate items in a collection
        /// </summary>
        /// <param name="ma">Collection to iterate</param>
        /// <param name="f">Function to apply to each item in the collection</param>
        /// <returns>Unit</returns>
        public static Aff<Env, Unit> iter<Env, A>(Eff<Env, Seq<A>> ma, Func<A, Aff<Env, Unit>> f) where Env : struct, HasCancel<Env> =>
            AffMaybe<Env, Unit>(async env =>
            {
                var xs = ma.Run(env);
                if (xs.IsFail) return xs.Cast<Unit>();

                foreach (var x in xs.Value)
                {
                    var r = await f(x).Run(env);
                    if (r.IsFail) return r.Cast<Unit>();
                }

                return Fin<Unit>.Succ(default);
            });

        /// <summary>
        /// Sequentially iterate items in a collection
        /// </summary>
        /// <param name="ma">Collection to iterate</param>
        /// <param name="f">Function to apply to each item in the collection</param>
        /// <returns>Unit</returns>
        public static Aff<Env, Unit> iter<Env, A>(Eff<Env, Seq<A>> ma, Func<A, Aff<Unit>> f) where Env : struct, HasCancel<Env> =>
            AffMaybe<Env, Unit>(async env =>
            {
                var xs = ma.Run(env);
                if (xs.IsFail) return xs.Cast<Unit>();

                foreach (var x in xs.Value)
                {
                    var r = await f(x).Run();
                    if (r.IsFail) return r.Cast<Unit>();
                }

                return Fin<Unit>.Succ(default);
            });
 
        /// <summary>
        /// Sequentially iterate items in a collection
        /// </summary>
        /// <param name="ma">Collection to iterate</param>
        /// <param name="f">Function to apply to each item in the collection</param>
        /// <returns>Unit</returns>
        public static Eff<Env, Unit> iter<Env, A>(Eff<Env, Seq<A>> ma, Func<A, Eff<Env, Unit>> f) where Env : struct, HasCancel<Env> =>
            EffMaybe<Env, Unit>(env =>
            {
                var xs = ma.Run(env);
                if (xs.IsFail) return xs.Cast<Unit>();

                foreach (var x in xs.Value)
                {
                    var r = f(x).Run(env);
                    if (r.IsFail) return r.Cast<Unit>();
                }

                return Fin<Unit>.Succ(default);
            });
 
        /// <summary>
        /// Sequentially iterate items in a collection
        /// </summary>
        /// <param name="ma">Collection to iterate</param>
        /// <param name="f">Function to apply to each item in the collection</param>
        /// <returns>Unit</returns>
        public static Eff<Env, Unit> iter<Env, A>(Eff<Env, Seq<A>> ma, Func<A, Eff<Unit>> f) where Env : struct, HasCancel<Env> =>
            EffMaybe<Env, Unit>(env =>
            {
                var xs = ma.Run(env);
                if (xs.IsFail) return xs.Cast<Unit>();

                foreach (var x in xs.Value)
                {
                    var r = f(x).Run();
                    if (r.IsFail) return r.Cast<Unit>();
                }

                return Fin<Unit>.Succ(default);
            });
        
        /// <summary>
        /// Sequentially iterate items in a collection
        /// </summary>
        /// <param name="ma">Collection to iterate</param>
        /// <param name="f">Function to apply to each item in the collection</param>
        /// <returns>Unit</returns>
        public static Aff<Env, Unit> iter<Env, A>(Eff<Seq<A>> ma, Func<A, Aff<Env, Unit>> f) where Env : struct, HasCancel<Env> =>
            AffMaybe<Env, Unit>(async env =>
            {
                var xs = ma.Run();
                if (xs.IsFail) return xs.Cast<Unit>();

                foreach (var x in xs.Value)
                {
                    var r = await f(x).Run(env);
                    if (r.IsFail) return r.Cast<Unit>();
                }

                return Fin<Unit>.Succ(default);
            });

        /// <summary>
        /// Sequentially iterate items in a collection
        /// </summary>
        /// <param name="ma">Collection to iterate</param>
        /// <param name="f">Function to apply to each item in the collection</param>
        /// <returns>Unit</returns>
        public static Aff<Unit> iter<A>(Eff<Seq<A>> ma, Func<A, Aff<Unit>> f) =>
            AffMaybe(async () =>
            {
                var xs = ma.Run();
                if (xs.IsFail) return xs.Cast<Unit>();

                foreach (var x in xs.Value)
                {
                    var r = await f(x).Run();
                    if (r.IsFail) return r.Cast<Unit>();
                }

                return Fin<Unit>.Succ(default);
            });
 
        /// <summary>
        /// Sequentially iterate items in a collection
        /// </summary>
        /// <param name="ma">Collection to iterate</param>
        /// <param name="f">Function to apply to each item in the collection</param>
        /// <returns>Unit</returns>
        public static Eff<Env, Unit> iter<Env, A>(Eff<Seq<A>> ma, Func<A, Eff<Env, Unit>> f) where Env : struct, HasCancel<Env> =>
            EffMaybe<Env, Unit>(env =>
            {
                var xs = ma.Run();
                if (xs.IsFail) return xs.Cast<Unit>();

                foreach (var x in xs.Value)
                {
                    var r = f(x).Run(env);
                    if (r.IsFail) return r.Cast<Unit>();
                }

                return Fin<Unit>.Succ(default);
            });
 
        /// <summary>
        /// Sequentially iterate items in a collection
        /// </summary>
        /// <param name="ma">Collection to iterate</param>
        /// <param name="f">Function to apply to each item in the collection</param>
        /// <returns>Unit</returns>
        public static Eff<Unit> iter<A>(Eff<Seq<A>> ma, Func<A, Eff<Unit>> f) =>
            EffMaybe(() =>
            {
                var xs = ma.Run();
                if (xs.IsFail) return xs.Cast<Unit>();

                foreach (var x in xs.Value)
                {
                    var r = f(x).Run();
                    if (r.IsFail) return r.Cast<Unit>();
                }

                return Fin<Unit>.Succ(default);
            });
        

        
        
        /// <summary>
        /// Sequentially iterate items in a collection
        /// </summary>
        /// <param name="ma">Collection to iterate</param>
        /// <param name="f">Function to apply to each item in the collection</param>
        /// <returns>Unit</returns>
        public static Aff<Env, Unit> iter<Env, A>(Eff<Env, Seq<Aff<Env, A>>> ma, Func<A, Aff<Env, Unit>> f) where Env : struct, HasCancel<Env> =>
            AffMaybe<Env, Unit>(async env =>
            {
                var xs = ma.Run(env);
                if (xs.IsFail) return xs.Cast<Unit>();

                foreach (var iox in xs.Value)
                {
                    var x = await iox.Run(env);
                    if (x.IsFail) return x.Cast<Unit>();
                    var r = await f(x.Value).Run(env);
                    if (r.IsFail) return r.Cast<Unit>();
                }

                return Fin<Unit>.Succ(default);
            });

        /// <summary>
        /// Sequentially iterate items in a collection
        /// </summary>
        /// <param name="ma">Collection to iterate</param>
        /// <param name="f">Function to apply to each item in the collection</param>
        /// <returns>Unit</returns>
        public static Aff<Env, Unit> iter<Env, A>(Eff<Env, Seq<Aff<Env, A>>> ma, Func<A, Aff<Unit>> f) where Env : struct, HasCancel<Env> =>
            AffMaybe<Env, Unit>(async env =>
            {
                var xs = ma.Run(env);
                if (xs.IsFail) return xs.Cast<Unit>();

                foreach (var iox in xs.Value)
                {
                    var x = await iox.Run(env);
                    if (x.IsFail) return x.Cast<Unit>();
                    var r = await f(x.Value).Run();
                    if (r.IsFail) return r.Cast<Unit>();
                }

                return Fin<Unit>.Succ(default);
            });
 
        /// <summary>
        /// Sequentially iterate items in a collection
        /// </summary>
        /// <param name="ma">Collection to iterate</param>
        /// <param name="f">Function to apply to each item in the collection</param>
        /// <returns>Unit</returns>
        public static Aff<Env, Unit> iter<Env, A>(Eff<Env, Seq<Aff<Env, A>>> ma, Func<A, Eff<Env, Unit>> f) where Env : struct, HasCancel<Env> =>
            AffMaybe<Env, Unit>(async env =>
            {
                var xs = ma.Run(env);
                if (xs.IsFail) return xs.Cast<Unit>();

                foreach (var iox in xs.Value)
                {
                    var x = await iox.Run(env);
                    if (x.IsFail) return x.Cast<Unit>();
                    var r = f(x.Value).Run(env);
                    if (r.IsFail) return r.Cast<Unit>();
                }

                return Fin<Unit>.Succ(default);
            });
 
        /// <summary>
        /// Sequentially iterate items in a collection
        /// </summary>
        /// <param name="ma">Collection to iterate</param>
        /// <param name="f">Function to apply to each item in the collection</param>
        /// <returns>Unit</returns>
        public static Aff<Env, Unit> iter<Env, A>(Eff<Env, Seq<Aff<Env, A>>> ma, Func<A, Eff<Unit>> f) where Env : struct, HasCancel<Env> =>
            AffMaybe<Env, Unit>(async env =>
            {
                var xs = ma.Run(env);
                if (xs.IsFail) return xs.Cast<Unit>();

                foreach (var iox in xs.Value)
                {
                    var x = await iox.Run(env);
                    if (x.IsFail) return x.Cast<Unit>();
                    var r = f(x.Value).Run();
                    if (r.IsFail) return r.Cast<Unit>();
                }

                return Fin<Unit>.Succ(default);
            });
        
        /// <summary>
        /// Sequentially iterate items in a collection
        /// </summary>
        /// <param name="ma">Collection to iterate</param>
        /// <param name="f">Function to apply to each item in the collection</param>
        /// <returns>Unit</returns>
        public static Aff<Env, Unit> iter<Env, A>(Eff<Seq<Aff<Env, A>>> ma, Func<A, Aff<Env, Unit>> f) where Env : struct, HasCancel<Env> =>
            AffMaybe<Env, Unit>(async env =>
            {
                var xs = ma.Run();
                if (xs.IsFail) return xs.Cast<Unit>();

                foreach (var iox in xs.Value)
                {
                    var x = await iox.Run(env);
                    if (x.IsFail) return x.Cast<Unit>();
                    var r = await f(x.Value).Run(env);
                    if (r.IsFail) return r.Cast<Unit>();
                }

                return Fin<Unit>.Succ(default);
            });

        /// <summary>
        /// Sequentially iterate items in a collection
        /// </summary>
        /// <param name="ma">Collection to iterate</param>
        /// <param name="f">Function to apply to each item in the collection</param>
        /// <returns>Unit</returns>
        public static Aff<Env, Unit> iter<Env, A>(Eff<Seq<Aff<Env, A>>> ma, Func<A, Aff<Unit>> f) where Env : struct, HasCancel<Env> =>
            AffMaybe<Env, Unit>(async env =>
            {
                var xs = ma.Run();
                if (xs.IsFail) return xs.Cast<Unit>();

                foreach (var iox in xs.Value)
                {
                    var x = await iox.Run(env);
                    if (x.IsFail) return x.Cast<Unit>();
                    var r = await f(x.Value).Run();
                    if (r.IsFail) return r.Cast<Unit>();
                }

                return Fin<Unit>.Succ(default);
            });
 
        /// <summary>
        /// Sequentially iterate items in a collection
        /// </summary>
        /// <param name="ma">Collection to iterate</param>
        /// <param name="f">Function to apply to each item in the collection</param>
        /// <returns>Unit</returns>
        public static Aff<Env, Unit> iter<Env, A>(Eff<Seq<Aff<Env, A>>> ma, Func<A, Eff<Env, Unit>> f) where Env : struct, HasCancel<Env> =>
            AffMaybe<Env, Unit>(async env =>
            {
                var xs = ma.Run();
                if (xs.IsFail) return xs.Cast<Unit>();

                foreach (var iox in xs.Value)
                {
                    var x = await iox.Run(env);
                    if (x.IsFail) return x.Cast<Unit>();
                    var r = f(x.Value).Run(env);
                    if (r.IsFail) return r.Cast<Unit>();
                }

                return Fin<Unit>.Succ(default);
            });
 
        /// <summary>
        /// Sequentially iterate items in a collection
        /// </summary>
        /// <param name="ma">Collection to iterate</param>
        /// <param name="f">Function to apply to each item in the collection</param>
        /// <returns>Unit</returns>
        public static Aff<Env, Unit> iter<Env, A>(Eff<Seq<Aff<Env, A>>> ma, Func<A, Eff<Unit>> f) where Env : struct, HasCancel<Env> =>
            AffMaybe<Env, Unit>(async env =>
            {
                var xs = ma.Run();
                if (xs.IsFail) return xs.Cast<Unit>();

                foreach (var iox in xs.Value)
                {
                    var x = await iox.Run(env);
                    if (x.IsFail) return x.Cast<Unit>();
                    var r = f(x.Value).Run();
                    if (r.IsFail) return r.Cast<Unit>();
                }

                return Fin<Unit>.Succ(default);
            });        
         

        
        
        /// <summary>
        /// Sequentially iterate items in a collection
        /// </summary>
        /// <param name="ma">Collection to iterate</param>
        /// <param name="f">Function to apply to each item in the collection</param>
        /// <returns>Unit</returns>
        public static Aff<Env, Unit> iter<Env, A>(Eff<Env, Seq<Aff<A>>> ma, Func<A, Aff<Env, Unit>> f) where Env : struct, HasCancel<Env> =>
            AffMaybe<Env, Unit>(async env =>
            {
                var xs = ma.Run(env);
                if (xs.IsFail) return xs.Cast<Unit>();

                foreach (var iox in xs.Value)
                {
                    var x = await iox.Run();
                    if (x.IsFail) return x.Cast<Unit>();
                    var r = await f(x.Value).Run(env);
                    if (r.IsFail) return r.Cast<Unit>();
                }

                return Fin<Unit>.Succ(default);
            });

        /// <summary>
        /// Sequentially iterate items in a collection
        /// </summary>
        /// <param name="ma">Collection to iterate</param>
        /// <param name="f">Function to apply to each item in the collection</param>
        /// <returns>Unit</returns>
        public static Aff<Env, Unit> iter<Env, A>(Eff<Env, Seq<Aff<A>>> ma, Func<A, Aff<Unit>> f) where Env : struct, HasCancel<Env> =>
            AffMaybe<Env, Unit>(async env =>
            {
                var xs = ma.Run(env);
                if (xs.IsFail) return xs.Cast<Unit>();

                foreach (var iox in xs.Value)
                {
                    var x = await iox.Run();
                    if (x.IsFail) return x.Cast<Unit>();
                    var r = await f(x.Value).Run();
                    if (r.IsFail) return r.Cast<Unit>();
                }

                return Fin<Unit>.Succ(default);
            });
 
        /// <summary>
        /// Sequentially iterate items in a collection
        /// </summary>
        /// <param name="ma">Collection to iterate</param>
        /// <param name="f">Function to apply to each item in the collection</param>
        /// <returns>Unit</returns>
        public static Aff<Env, Unit> iter<Env, A>(Eff<Env, Seq<Aff<A>>> ma, Func<A, Eff<Env, Unit>> f) where Env : struct, HasCancel<Env> =>
            AffMaybe<Env, Unit>(async env =>
            {
                var xs = ma.Run(env);
                if (xs.IsFail) return xs.Cast<Unit>();

                foreach (var iox in xs.Value)
                {
                    var x = await iox.Run();
                    if (x.IsFail) return x.Cast<Unit>();
                    var r = f(x.Value).Run(env);
                    if (r.IsFail) return r.Cast<Unit>();
                }

                return Fin<Unit>.Succ(default);
            });
 
        /// <summary>
        /// Sequentially iterate items in a collection
        /// </summary>
        /// <param name="ma">Collection to iterate</param>
        /// <param name="f">Function to apply to each item in the collection</param>
        /// <returns>Unit</returns>
        public static Aff<Env, Unit> iter<Env, A>(Eff<Env, Seq<Aff<A>>> ma, Func<A, Eff<Unit>> f) where Env : struct, HasCancel<Env> =>
            AffMaybe<Env, Unit>(async env =>
            {
                var xs = ma.Run(env);
                if (xs.IsFail) return xs.Cast<Unit>();

                foreach (var iox in xs.Value)
                {
                    var x = await iox.Run();
                    if (x.IsFail) return x.Cast<Unit>();
                    var r = f(x.Value).Run();
                    if (r.IsFail) return r.Cast<Unit>();
                }

                return Fin<Unit>.Succ(default);
            });
        
        /// <summary>
        /// Sequentially iterate items in a collection
        /// </summary>
        /// <param name="ma">Collection to iterate</param>
        /// <param name="f">Function to apply to each item in the collection</param>
        /// <returns>Unit</returns>
        public static Aff<Env, Unit> iter<Env, A>(Eff<Seq<Aff<A>>> ma, Func<A, Aff<Env, Unit>> f) where Env : struct, HasCancel<Env> =>
            AffMaybe<Env, Unit>(async env =>
            {
                var xs = ma.Run();
                if (xs.IsFail) return xs.Cast<Unit>();

                foreach (var iox in xs.Value)
                {
                    var x = await iox.Run();
                    if (x.IsFail) return x.Cast<Unit>();
                    var r = await f(x.Value).Run(env);
                    if (r.IsFail) return r.Cast<Unit>();
                }

                return Fin<Unit>.Succ(default);
            });

        /// <summary>
        /// Sequentially iterate items in a collection
        /// </summary>
        /// <param name="ma">Collection to iterate</param>
        /// <param name="f">Function to apply to each item in the collection</param>
        /// <returns>Unit</returns>
        public static Aff<Unit> iter<A>(Eff<Seq<Aff<A>>> ma, Func<A, Aff<Unit>> f) =>
            AffMaybe(async () =>
            {
                var xs = ma.Run();
                if (xs.IsFail) return xs.Cast<Unit>();

                foreach (var iox in xs.Value)
                {
                    var x = await iox.Run();
                    if (x.IsFail) return x.Cast<Unit>();
                    var r = await f(x.Value).Run();
                    if (r.IsFail) return r.Cast<Unit>();
                }

                return Fin<Unit>.Succ(default);
            });
 
        /// <summary>
        /// Sequentially iterate items in a collection
        /// </summary>
        /// <param name="ma">Collection to iterate</param>
        /// <param name="f">Function to apply to each item in the collection</param>
        /// <returns>Unit</returns>
        public static Aff<Env, Unit> iter<Env, A>(Eff<Seq<Aff<A>>> ma, Func<A, Eff<Env, Unit>> f) where Env : struct, HasCancel<Env> =>
            AffMaybe<Env, Unit>(async env =>
            {
                var xs = ma.Run();
                if (xs.IsFail) return xs.Cast<Unit>();

                foreach (var iox in xs.Value)
                {
                    var x = await iox.Run();
                    if (x.IsFail) return x.Cast<Unit>();
                    var r = f(x.Value).Run(env);
                    if (r.IsFail) return r.Cast<Unit>();
                }

                return Fin<Unit>.Succ(default);
            });
 
        /// <summary>
        /// Sequentially iterate items in a collection
        /// </summary>
        /// <param name="ma">Collection to iterate</param>
        /// <param name="f">Function to apply to each item in the collection</param>
        /// <returns>Unit</returns>
        public static Aff<Unit> iter<A>(Eff<Seq<Aff<A>>> ma, Func<A, Eff<Unit>> f) =>
            AffMaybe(async () =>
            {
                var xs = ma.Run();
                if (xs.IsFail) return xs.Cast<Unit>();

                foreach (var iox in xs.Value)
                {
                    var x = await iox.Run();
                    if (x.IsFail) return x.Cast<Unit>();
                    var r = f(x.Value).Run();
                    if (r.IsFail) return r.Cast<Unit>();
                }

                return Fin<Unit>.Succ(default);
            });   
    
    
    
    
            
        /// <summary>
        /// Sequentially iterate items in a collection
        /// </summary>
        /// <param name="ma">Collection to iterate</param>
        /// <param name="f">Function to apply to each item in the collection</param>
        /// <returns>Unit</returns>
        public static Aff<Env, Unit> iter<Env, A>(Eff<Env, Seq<Eff<Env, A>>> ma, Func<A, Aff<Env, Unit>> f) where Env : struct, HasCancel<Env> =>
            AffMaybe<Env, Unit>(async env =>
            {
                var xs = ma.Run(env);
                if (xs.IsFail) return xs.Cast<Unit>();

                foreach (var iox in xs.Value)
                {
                    var x = iox.Run(env);
                    if (x.IsFail) return x.Cast<Unit>();
                    var r = await f(x.Value).Run(env);
                    if (r.IsFail) return r.Cast<Unit>();
                }

                return Fin<Unit>.Succ(default);
            });

        /// <summary>
        /// Sequentially iterate items in a collection
        /// </summary>
        /// <param name="ma">Collection to iterate</param>
        /// <param name="f">Function to apply to each item in the collection</param>
        /// <returns>Unit</returns>
        public static Aff<Env, Unit> iter<Env, A>(Eff<Env, Seq<Eff<Env, A>>> ma, Func<A, Aff<Unit>> f) where Env : struct, HasCancel<Env> =>
            AffMaybe<Env, Unit>(async env =>
            {
                var xs = ma.Run(env);
                if (xs.IsFail) return xs.Cast<Unit>();

                foreach (var iox in xs.Value)
                {
                    var x = iox.Run(env);
                    if (x.IsFail) return x.Cast<Unit>();
                    var r = await f(x.Value).Run();
                    if (r.IsFail) return r.Cast<Unit>();
                }

                return Fin<Unit>.Succ(default);
            });
 
        /// <summary>
        /// Sequentially iterate items in a collection
        /// </summary>
        /// <param name="ma">Collection to iterate</param>
        /// <param name="f">Function to apply to each item in the collection</param>
        /// <returns>Unit</returns>
        public static Eff<Env, Unit> iter<Env, A>(Eff<Env, Seq<Eff<Env, A>>> ma, Func<A, Eff<Env, Unit>> f) where Env : struct, HasCancel<Env> =>
            EffMaybe<Env, Unit>(env =>
            {
                var xs = ma.Run(env);
                if (xs.IsFail) return xs.Cast<Unit>();

                foreach (var iox in xs.Value)
                {
                    var x = iox.Run(env);
                    if (x.IsFail) return x.Cast<Unit>();
                    var r = f(x.Value).Run(env);
                    if (r.IsFail) return r.Cast<Unit>();
                }

                return Fin<Unit>.Succ(default);
            });
 
        /// <summary>
        /// Sequentially iterate items in a collection
        /// </summary>
        /// <param name="ma">Collection to iterate</param>
        /// <param name="f">Function to apply to each item in the collection</param>
        /// <returns>Unit</returns>
        public static Eff<Env, Unit> iter<Env, A>(Eff<Env, Seq<Eff<Env, A>>> ma, Func<A, Eff<Unit>> f) where Env : struct, HasCancel<Env> =>
            EffMaybe<Env, Unit>(env =>
            {
                var xs = ma.Run(env);
                if (xs.IsFail) return xs.Cast<Unit>();

                foreach (var iox in xs.Value)
                {
                    var x = iox.Run(env);
                    if (x.IsFail) return x.Cast<Unit>();
                    var r = f(x.Value).Run();
                    if (r.IsFail) return r.Cast<Unit>();
                }

                return Fin<Unit>.Succ(default);
            });
        
        /// <summary>
        /// Sequentially iterate items in a collection
        /// </summary>
        /// <param name="ma">Collection to iterate</param>
        /// <param name="f">Function to apply to each item in the collection</param>
        /// <returns>Unit</returns>
        public static Aff<Env, Unit> iter<Env, A>(Eff<Seq<Eff<Env, A>>> ma, Func<A, Aff<Env, Unit>> f) where Env : struct, HasCancel<Env> =>
            AffMaybe<Env, Unit>(async env =>
            {
                var xs = ma.Run();
                if (xs.IsFail) return xs.Cast<Unit>();

                foreach (var iox in xs.Value)
                {
                    var x = iox.Run(env);
                    if (x.IsFail) return x.Cast<Unit>();
                    var r = await f(x.Value).Run(env);
                    if (r.IsFail) return r.Cast<Unit>();
                }

                return Fin<Unit>.Succ(default);
            });

        /// <summary>
        /// Sequentially iterate items in a collection
        /// </summary>
        /// <param name="ma">Collection to iterate</param>
        /// <param name="f">Function to apply to each item in the collection</param>
        /// <returns>Unit</returns>
        public static Aff<Env, Unit> iter<Env, A>(Eff<Seq<Eff<Env, A>>> ma, Func<A, Aff<Unit>> f) where Env : struct, HasCancel<Env> =>
            AffMaybe<Env, Unit>(async env =>
            {
                var xs = ma.Run();
                if (xs.IsFail) return xs.Cast<Unit>();

                foreach (var iox in xs.Value)
                {
                    var x = iox.Run(env);
                    if (x.IsFail) return x.Cast<Unit>();
                    var r = await f(x.Value).Run();
                    if (r.IsFail) return r.Cast<Unit>();
                }

                return Fin<Unit>.Succ(default);
            });
 
        /// <summary>
        /// Sequentially iterate items in a collection
        /// </summary>
        /// <param name="ma">Collection to iterate</param>
        /// <param name="f">Function to apply to each item in the collection</param>
        /// <returns>Unit</returns>
        public static Eff<Env, Unit> iter<Env, A>(Eff<Seq<Eff<Env, A>>> ma, Func<A, Eff<Env, Unit>> f) where Env : struct, HasCancel<Env> =>
            EffMaybe<Env, Unit>(env =>
            {
                var xs = ma.Run();
                if (xs.IsFail) return xs.Cast<Unit>();

                foreach (var iox in xs.Value)
                {
                    var x = iox.Run(env);
                    if (x.IsFail) return x.Cast<Unit>();
                    var r = f(x.Value).Run(env);
                    if (r.IsFail) return r.Cast<Unit>();
                }

                return Fin<Unit>.Succ(default);
            });
 
        /// <summary>
        /// Sequentially iterate items in a collection
        /// </summary>
        /// <param name="ma">Collection to iterate</param>
        /// <param name="f">Function to apply to each item in the collection</param>
        /// <returns>Unit</returns>
        public static Eff<Env, Unit> iter<Env, A>(Eff<Seq<Eff<Env, A>>> ma, Func<A, Eff<Unit>> f) where Env : struct, HasCancel<Env> =>
            EffMaybe<Env, Unit>(env =>
            {
                var xs = ma.Run();
                if (xs.IsFail) return xs.Cast<Unit>();

                foreach (var iox in xs.Value)
                {
                    var x = iox.Run(env);
                    if (x.IsFail) return x.Cast<Unit>();
                    var r = f(x.Value).Run();
                    if (r.IsFail) return r.Cast<Unit>();
                }

                return Fin<Unit>.Succ(default);
            });        
         
    
        
 
    
            
        /// <summary>
        /// Sequentially iterate items in a collection
        /// </summary>
        /// <param name="ma">Collection to iterate</param>
        /// <param name="f">Function to apply to each item in the collection</param>
        /// <returns>Unit</returns>
        public static Aff<Env, Unit> iter<Env, A>(Eff<Env, Seq<Eff<A>>> ma, Func<A, Aff<Env, Unit>> f) where Env : struct, HasCancel<Env> =>
            AffMaybe<Env, Unit>(async env =>
            {
                var xs = ma.Run(env);
                if (xs.IsFail) return xs.Cast<Unit>();

                foreach (var iox in xs.Value)
                {
                    var x = iox.Run();
                    if (x.IsFail) return x.Cast<Unit>();
                    var r = await f(x.Value).Run(env);
                    if (r.IsFail) return r.Cast<Unit>();
                }

                return Fin<Unit>.Succ(default);
            });

        /// <summary>
        /// Sequentially iterate items in a collection
        /// </summary>
        /// <param name="ma">Collection to iterate</param>
        /// <param name="f">Function to apply to each item in the collection</param>
        /// <returns>Unit</returns>
        public static Aff<Env, Unit> iter<Env, A>(Eff<Env, Seq<Eff<A>>> ma, Func<A, Aff<Unit>> f) where Env : struct, HasCancel<Env> =>
            AffMaybe<Env, Unit>(async env =>
            {
                var xs = ma.Run(env);
                if (xs.IsFail) return xs.Cast<Unit>();

                foreach (var iox in xs.Value)
                {
                    var x = iox.Run();
                    if (x.IsFail) return x.Cast<Unit>();
                    var r = await f(x.Value).Run();
                    if (r.IsFail) return r.Cast<Unit>();
                }

                return Fin<Unit>.Succ(default);
            });
 
        /// <summary>
        /// Sequentially iterate items in a collection
        /// </summary>
        /// <param name="ma">Collection to iterate</param>
        /// <param name="f">Function to apply to each item in the collection</param>
        /// <returns>Unit</returns>
        public static Eff<Env, Unit> iter<Env, A>(Eff<Env, Seq<Eff<A>>> ma, Func<A, Eff<Env, Unit>> f) where Env : struct, HasCancel<Env> =>
            EffMaybe<Env, Unit>(env =>
            {
                var xs = ma.Run(env);
                if (xs.IsFail) return xs.Cast<Unit>();

                foreach (var iox in xs.Value)
                {
                    var x = iox.Run();
                    if (x.IsFail) return x.Cast<Unit>();
                    var r = f(x.Value).Run(env);
                    if (r.IsFail) return r.Cast<Unit>();
                }

                return Fin<Unit>.Succ(default);
            });
 
        /// <summary>
        /// Sequentially iterate items in a collection
        /// </summary>
        /// <param name="ma">Collection to iterate</param>
        /// <param name="f">Function to apply to each item in the collection</param>
        /// <returns>Unit</returns>
        public static Eff<Env, Unit> iter<Env, A>(Eff<Env, Seq<Eff<A>>> ma, Func<A, Eff<Unit>> f) where Env : struct, HasCancel<Env> =>
            EffMaybe<Env, Unit>(env =>
            {
                var xs = ma.Run(env);
                if (xs.IsFail) return xs.Cast<Unit>();

                foreach (var iox in xs.Value)
                {
                    var x = iox.Run();
                    if (x.IsFail) return x.Cast<Unit>();
                    var r = f(x.Value).Run();
                    if (r.IsFail) return r.Cast<Unit>();
                }

                return Fin<Unit>.Succ(default);
            });
        
        /// <summary>
        /// Sequentially iterate items in a collection
        /// </summary>
        /// <param name="ma">Collection to iterate</param>
        /// <param name="f">Function to apply to each item in the collection</param>
        /// <returns>Unit</returns>
        public static Aff<Env, Unit> iter<Env, A>(Eff<Seq<Eff<A>>> ma, Func<A, Aff<Env, Unit>> f) where Env : struct, HasCancel<Env> =>
            AffMaybe<Env, Unit>(async env =>
            {
                var xs = ma.Run();
                if (xs.IsFail) return xs.Cast<Unit>();

                foreach (var iox in xs.Value)
                {
                    var x = iox.Run();
                    if (x.IsFail) return x.Cast<Unit>();
                    var r = await f(x.Value).Run(env);
                    if (r.IsFail) return r.Cast<Unit>();
                }

                return Fin<Unit>.Succ(default);
            });

        /// <summary>
        /// Sequentially iterate items in a collection
        /// </summary>
        /// <param name="ma">Collection to iterate</param>
        /// <param name="f">Function to apply to each item in the collection</param>
        /// <returns>Unit</returns>
        public static Aff<Unit> iter<A>(Eff<Seq<Eff<A>>> ma, Func<A, Aff<Unit>> f) =>
            AffMaybe(async () =>
            {
                var xs = ma.Run();
                if (xs.IsFail) return xs.Cast<Unit>();

                foreach (var iox in xs.Value)
                {
                    var x = iox.Run();
                    if (x.IsFail) return x.Cast<Unit>();
                    var r = await f(x.Value).Run();
                    if (r.IsFail) return r.Cast<Unit>();
                }

                return Fin<Unit>.Succ(default);
            });
 
        /// <summary>
        /// Sequentially iterate items in a collection
        /// </summary>
        /// <param name="ma">Collection to iterate</param>
        /// <param name="f">Function to apply to each item in the collection</param>
        /// <returns>Unit</returns>
        public static Eff<Env, Unit> iter<Env, A>(Eff<Seq<Eff<A>>> ma, Func<A, Eff<Env, Unit>> f) where Env : struct, HasCancel<Env> =>
            EffMaybe<Env, Unit>(env =>
            {
                var xs = ma.Run();
                if (xs.IsFail) return xs.Cast<Unit>();

                foreach (var iox in xs.Value)
                {
                    var x = iox.Run();
                    if (x.IsFail) return x.Cast<Unit>();
                    var r = f(x.Value).Run(env);
                    if (r.IsFail) return r.Cast<Unit>();
                }

                return Fin<Unit>.Succ(default);
            });
 
        /// <summary>
        /// Sequentially iterate items in a collection
        /// </summary>
        /// <param name="ma">Collection to iterate</param>
        /// <param name="f">Function to apply to each item in the collection</param>
        /// <returns>Unit</returns>
        public static Eff<Unit> iter<A>(Eff<Seq<Eff<A>>> ma, Func<A, Eff<Unit>> f) =>
            EffMaybe(() =>
            {
                var xs = ma.Run();
                if (xs.IsFail) return xs.Cast<Unit>();

                foreach (var iox in xs.Value)
                {
                    var x = iox.Run();
                    if (x.IsFail) return x.Cast<Unit>();
                    var r = f(x.Value).Run();
                    if (r.IsFail) return r.Cast<Unit>();
                }

                return Fin<Unit>.Succ(default);
            });        
        
    }
}