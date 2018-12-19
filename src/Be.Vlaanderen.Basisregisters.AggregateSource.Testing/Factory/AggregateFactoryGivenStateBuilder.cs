namespace Be.Vlaanderen.Basisregisters.AggregateSource.Testing.Factory
{
    using System;
    using System.Linq;

    internal class AggregateFactoryGivenStateBuilder<TAggregateRoot> : IAggregateFactoryGivenStateBuilder<TAggregateRoot>
        where TAggregateRoot : IAggregateRootEntity
    {
        private readonly Func<IAggregateRootEntity> _sutFactory;
        private readonly object[] _givens;

        public AggregateFactoryGivenStateBuilder(Func<IAggregateRootEntity> sutFactory, object[] givens)
        {
            _sutFactory = sutFactory;
            _givens = givens;
        }

        public IAggregateFactoryGivenStateBuilder<TAggregateRoot> Given(params object[] events)
        {
            if (events == null)
                throw new ArgumentNullException(nameof(events));

            return new AggregateFactoryGivenStateBuilder<TAggregateRoot>(_sutFactory, _givens.Concat(events).ToArray());
        }

        public IAggregateFactoryWhenStateBuilder When<TAggregateRootResult>(
            Func<TAggregateRoot, TAggregateRootResult> factory) where TAggregateRootResult : IAggregateRootEntity
        {
            if (factory == null)
                throw new ArgumentNullException(nameof(factory));

            return new AggregateFactoryWhenStateBuilder(_sutFactory, _givens, root => factory((TAggregateRoot) root));
        }
    }
}
