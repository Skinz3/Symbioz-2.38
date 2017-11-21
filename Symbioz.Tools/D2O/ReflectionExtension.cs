using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Symbioz.Tools.D2O
{
    static class ReflectionExtensions // Classe de bouh²
    {
        #region Public methods
        public static T CreateDelegate<T>(this ConstructorInfo ctor)
        {
            var parameters = ctor.GetParameters().Select(param => Expression.Parameter(param.ParameterType)).ToList();

            var lamba = Expression.Lambda<T>(Expression.New(ctor, parameters), parameters);

            return lamba.Compile();
        }
        #endregion
    }
}
