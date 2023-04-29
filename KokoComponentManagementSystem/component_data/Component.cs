namespace KokoComponentManagementSystem.component_data;
public abstract class Component {
    public int Identifier { get; }
    protected Component(int identifier) => Identifier = identifier;
}
