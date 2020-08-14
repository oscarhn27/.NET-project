using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Castle.DynamicProxy;
using NLog;
using Services.Common;

namespace Services.Interceptors
{
    public class TransactionalInterceptor : IInterceptor
    {
        
        private static Logger _logger;
        public TransactionalInterceptor()
        {
            _logger =LogManager.GetCurrentClassLogger();                   
        }

        public void Intercept(IInvocation invocation)
        {
            BaseService srv = (BaseService)invocation.InvocationTarget;
            var name = $"{invocation.Method.DeclaringType}.{invocation.Method.Name}";
            var args = string.Join(", ", invocation.Arguments.Select(a => (a ?? "").ToString()));
            _logger.Info($"Entrando en metodo: {name}");
            _logger.Info($"Parametros: {args}");
            var watch = System.Diagnostics.Stopwatch.StartNew();

            try
            {
               
                srv.getIUnitOfWork().BeginTransaction();
                invocation.Proceed(); //Intercepted method is executed here.
                srv.getIUnitOfWork().Commit();
            }
            catch (Exception exce)
            {
                _logger.Error(exce, "Error producido", args);
                srv.getIUnitOfWork().Rollback();
                throw exce;
            }

            watch.Stop();
            var executionTime = watch.ElapsedMilliseconds;

            _logger.Info($"Saliendo de metodo {name}: resultado {invocation.ReturnValue}");
            _logger.Info($"Tiempo de ejecución: {executionTime} ms.");
            
        }
    }
}
