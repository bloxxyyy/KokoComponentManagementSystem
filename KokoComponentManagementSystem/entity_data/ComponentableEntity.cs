using System.Collections;
using KokoComponentManagementSystem.component_data;
using KokoComponentManagementSystem.error_handling;
using KokoEntityTreeSystem.src.entity_data;

namespace KokoComponentManagementSystem.entity_data;
public abstract class ComponentableEntity : Entity {

    private readonly Hashtable _components = new();

    public ComponentableEntity(int identifier) : base(identifier) {}

    #region CRUD operations

    public void AddComponent<T>(T component) where T : Component {
        if (_components.ContainsKey(typeof(T))) {
            ErrorHandler._handleWarning($"Component of type {typeof(T)} already exists on this entity!");
            return;
        }
        _components.Add(typeof(T), component);
    }

    public void RemoveComponent<T>() where T : Component {
        if (!_components.ContainsKey(typeof(T))) return;
        _components.Remove(typeof(T));
    }

    public T? GetComponent<T>() where T : Component {
        if (!_components.ContainsKey(typeof(T))) {
            ErrorHandler._handleError($"Component of type {typeof(T)} does not exist on this entity!");
            return null;
        }
        return (T)_components[typeof(T)];
    }

    public bool HasComponent<T>() where T : Component => _components.ContainsKey(typeof(T));

    public Component[] GetAllComponents() {
        var components = new Component[_components.Count];
        var i = 0;
        foreach (var component in _components.Values) {
            components[i++] = (Component)component;
        }
        return components;
    }

    #endregion

}
