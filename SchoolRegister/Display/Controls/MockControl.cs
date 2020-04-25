using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Register.Controls
{
    class MockControl
    {
		private Point point;
		private Size size;

		public Point Point
		{
			get { return point; }
			set { point = value; }
		}
		public Size Size
		{
			get { return size; }
			set { size = value; }
		}

		public MockControl(Point Location, Size Size)
		{
			this.Point = Location;
			this.Size = Size;
		}
	}
}
