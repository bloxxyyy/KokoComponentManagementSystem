using KokoComponentManagementSystem.error_handling;
using KokoComponentManagementSystem.entity_data;

namespace KokoComponentManagementSystem.component_data;

public static class ComponentFactory {
    public static T? CreateComponent<T>(ComponentableEntity entity, params object[] parameters) where T : Component {
        var constructor = typeof(T).GetConstructor(parameters.Select(arg => arg.GetType()).ToArray());
        if (constructor == null) {
            ErrorHandler._handleError(
                $"No constructor found for component of type {typeof(T)} with the given parameters!");
            return null;
        }
        
        entity.AddComponent((T)constructor.Invoke(parameters));
        return entity.GetComponent<T>();
    }
}
