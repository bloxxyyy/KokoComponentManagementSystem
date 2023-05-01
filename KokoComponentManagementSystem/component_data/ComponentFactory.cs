using KokoComponentManagementSystem.error_handling;
using KokoComponentManagementSystem.entity_data;
using KokoReflection;

namespace KokoComponentManagementSystem.component_data;

public static class ComponentFactory {

    public static T? CreateComponent<T>(ComponentableEntity entity, params object[] parameters) where T : Component {

        var parameterTypes = parameters.Select(arg => arg?.GetType() ?? typeof(object)).ToArray();
        var exactConstructor = Constructor.GetExactConstructor<T>(parameterTypes);
        
        if (exactConstructor == null) {
            ErrorHandler._handleError(
                $"No constructor found for component of type {typeof(T)} with the given parameters!");
            return null;
        }

        var instance = (T)exactConstructor.Invoke(parameters);
        entity.AddComponent(instance);
        return entity.GetComponent<T>();
    }
}