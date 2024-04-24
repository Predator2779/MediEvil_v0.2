using Character.Classes;
using Character.ComponentContainer;

namespace Character.CharacterControllers
{
    public abstract class Controller
    {
        protected readonly Person _person;
        protected PersonContainer _container;

        protected Controller(PersonContainer container)
        {
            _container = container;
            _person = new Person(container);
        }

        public virtual void Initialize() => _person.Initialize();
        public virtual void Execute() => _container.StateMachine.Execute();
        public virtual void FixedExecute() => _container.StateMachine.FixedExecute();
    }
}