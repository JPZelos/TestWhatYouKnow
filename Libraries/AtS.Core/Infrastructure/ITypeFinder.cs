using System;
using System.Collections.Generic;
using System.Reflection;

namespace TWYK.Core.Infrastructure
{
    /// <summary>
    /// Classes implementing this interface provide information about types
    /// to various services in the AthleticShop engine.
    /// </summary>
    public interface ITypeFinder
    {
        #region Methods

        IList<Assembly> GetAssemblies();

        IEnumerable<Type> FindClassesOfType(
            Type assignTypeFrom,
            bool onlyConcreteClasses = true
        );

        IEnumerable<Type> FindClassesOfType(
            Type assignTypeFrom,
            IEnumerable<Assembly> assemblies,
            bool onlyConcreteClasses = true
        );

        IEnumerable<Type> FindClassesOfType<T>(bool onlyConcreteClasses = true);

        IEnumerable<Type> FindClassesOfType<T>(
            IEnumerable<Assembly> assemblies,
            bool onlyConcreteClasses = true
        );

        #endregion
    }
}