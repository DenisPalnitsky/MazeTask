using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excelian.Maze.Test
{
    [TestFixture]
    public class GridNavigatorTest
    {
        [Test]
        public void SetCurrentPosition_throws_exception_if_outside_bounds()
        {
            GridNavigator navigator = new GridNavigator(10, 10);
            Assert.Throws<ArgumentOutOfRangeException>(() => { navigator.SetCurrentPosition(-1, -1); });
            Assert.Throws<ArgumentOutOfRangeException>(() => navigator.SetCurrentPosition(11, 0));
            Assert.Throws<ArgumentOutOfRangeException>(() => navigator.SetCurrentPosition(0, 11));
        }

        [Test]
        public void SetCurrentPosition_sets_position()
        {
            GridNavigator navigator = new GridNavigator(10, 10);
            navigator.SetCurrentPosition(1, 2);

            Assert.AreEqual(new Cell(1,2), navigator.CurrentPosition);
            
        }
                

        [Test]
        public void IsCellInBounds_returns_false_if_coordinates_is_outside_of_bounds()
        {
            GridNavigator navigator = new GridNavigator(10, 10);
            Assert.IsFalse(navigator.IsCellInBounds(11, 0));
            Assert.IsFalse(navigator.IsCellInBounds(10, 10));
            Assert.IsFalse(navigator.IsCellInBounds(0, 11));
            Assert.IsFalse(navigator.IsCellInBounds(-1, 0));
            Assert.IsFalse(navigator.IsCellInBounds(0, -1));            
        }

        [Test]
        public void IsCellInBounds_returns_true_if_coordinates_is_inside_of_bounds()
        {
            GridNavigator navigator = new GridNavigator(10, 10);
            Assert.IsTrue(navigator.IsCellInBounds(1, 2));            
            Assert.IsTrue(navigator.IsCellInBounds(9, 9));
            Assert.IsTrue(navigator.IsCellInBounds(0, 0));
        }

        [Test]
        public void Neighbours_returns_all_neighbour_cells()
        {
            GridNavigator navigator = new GridNavigator(10, 10);
            navigator.SetCurrentPosition(5, 5);

            Assert.AreEqual(new Cell(4, 5), navigator.Neighbours[Direction.Left]);
            Assert.AreEqual(new Cell(6, 5), navigator.Neighbours[Direction.Right]);
            Assert.AreEqual(new Cell(5, 6), navigator.Neighbours[Direction.Down]);
            Assert.AreEqual(new Cell(5, 4), navigator.Neighbours[Direction.Up]);
        }

        [Test]
        public void Neighbours_for_corner_cell_returns_only_available_neighbours()
        {
            GridNavigator navigator = new GridNavigator(10, 10);
            navigator.SetCurrentPosition(0, 0);

            Assert.AreEqual(2, navigator.Neighbours.Count);
            Assert.AreEqual(new Cell(1, 0), navigator.Neighbours[Direction.Right]);
            Assert.AreEqual(new Cell(0, 1), navigator.Neighbours[Direction.Down]);            
        }
    }
}
