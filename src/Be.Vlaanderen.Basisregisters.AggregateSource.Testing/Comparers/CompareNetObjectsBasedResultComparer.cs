namespace Be.Vlaanderen.Basisregisters.AggregateSource.Testing.Comparers
{
    using System;
    using System.Collections.Generic;
    using KellermanSoftware.CompareNetObjects;

    /// <summary>
    /// Compares results using a <see cref="T:KellermanSoftware.CompareNetObjects.ICompareLogic" /> object and reports the differences.
    /// </summary>
    public class CompareNetObjectsBasedResultComparer : IResultComparer
    {
        private readonly ICompareLogic _logic;

        /// <summary>
        /// Initializes a new instance of the <see cref="CompareNetObjectsBasedResultComparer"/> class.
        /// </summary>
        /// <param name="logic">The comparer.</param>
        /// <exception cref="System.ArgumentNullException">Thrown when the <paramref name="logic"/> is <c>null</c>.</exception>
        public CompareNetObjectsBasedResultComparer(ICompareLogic logic) => _logic = logic ?? throw new ArgumentNullException(nameof(logic));

        /// <summary>
        /// Compares the expected to the actual event.
        /// </summary>
        /// <param name="expected">The expected event.</param>
        /// <param name="actual">The actual event.</param>
        /// <returns>
        /// An enumeration of <see cref="T:Be.Vlaanderen.Basisregisters.AggregateSource.Testing.EventComparisonDifference">differences</see>, or empty if none found.
        /// </returns>
        public IEnumerable<ResultComparisonDifference> Compare(object expected, object actual)
        {
            var result = _logic.Compare(expected, actual);
            if (result.AreEqual)
                yield break;

            foreach (var difference in result.Differences)
            {
                yield return new ResultComparisonDifference(
                    expected, 
                    actual,
                    difference.ToString());
            }
        }
    }
}
