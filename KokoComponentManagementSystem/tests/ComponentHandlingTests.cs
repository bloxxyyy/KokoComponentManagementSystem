using KokoComponentManagementSystem.component_data;
using KokoComponentManagementSystem.entity_data;
using KokoComponentManagementSystem.error_handling;
using Moq;
using Xunit;

namespace KokoComponentManagementSystem.tests;
public class ComponentHandlingTests {

    private class TestComponent : Component {
        public TestComponent(int identifier) : base(identifier) { }
    }

    private class TestEntity : ComponentableEntity {
        public TestEntity(int identifier) : base(identifier) { }
    }

    [Fact]
    public void TestCreateComponent() {
        var entity = new TestEntity(1);
        var component = ComponentFactory.CreateComponent<TestComponent>(entity, 1);
        Assert.NotNull(component);
        Assert.Single(entity.GetAllComponents());
        Assert.True(entity.HasComponent<TestComponent>());
    }

    [Fact]
    public void TestDuplicateComponent() {
        var entity = new TestEntity(1);
        var errorHandlerMock = new Mock<Action<string>>();
        ErrorHandler._handleWarning = errorHandlerMock.Object;
        var component1 = ComponentFactory.CreateComponent<TestComponent>(entity, 1);
        var component2 = ComponentFactory.CreateComponent<TestComponent>(entity, 2);
        Assert.NotNull(component1);
        Assert.NotNull(component2); // This should just take the component1 since we got 1.
        Assert.Single(entity.GetAllComponents());
        Assert.True(entity.HasComponent<TestComponent>());
        errorHandlerMock.Verify( // verify we got a warning
            h => h.Invoke($"Component of type {typeof(TestComponent)} already exists on this entity!"),
            Times.Once);
    }


    [Fact]
    public void TestMissingConstructor() {
        var entity = new TestEntity(1);
        var errorHandlerMock = new Mock<Action<string>>();
        ErrorHandler._handleError = errorHandlerMock.Object;
        var component = ComponentFactory.CreateComponent<TestComponent>(entity, "string_param");
        errorHandlerMock.Verify(
            h => h.Invoke($"No constructor found for component of type {typeof(TestComponent)} with the given parameters!"),
            Times.Once);
        Assert.Null(component);
    }

    [Fact]
    public void TestMissingComponent() {
        var entity = new TestEntity(1);
        var errorHandlerMock = new Mock<Action<string>>();
        ErrorHandler._handleError = errorHandlerMock.Object;
        var component = entity.GetComponent<TestComponent>();
        errorHandlerMock.Verify(
            h => h.Invoke($"Component of type {typeof(TestComponent)} does not exist on this entity!"),
            Times.Once);
        Assert.Null(component);
    }

    [Fact]
    public void TestRemoveComponent() {
        var entity = new TestEntity(1);

        _ = ComponentFactory.CreateComponent<TestComponent>(entity);
        entity.RemoveComponent<TestComponent>();
        Assert.Empty(entity.GetAllComponents());
        Assert.False(entity.HasComponent<TestComponent>());
    }

}
