using System;
using MyNumber.Number;

namespace MyNumber.Vector
{
  public class TwoDirectionVector
  {
    protected DecimalNumber x;
    protected DecimalNumber y;

    public DecimalNumber X
    {
      get { return x; }
      set { x = value; }
    }

    public DecimalNumber Y
    {
      get { return y; }
      set { y = value; }
    }

    public TwoDirectionVector(DecimalNumber x, DecimalNumber y)
    {
      this.x = x;
      this.y = y;
    }

    public bool IsEqual(TwoDirectionVector vector)
    {
      return (this.X == vector.X) && (this.Y == vector.Y);
    }

    public TwoDirectionVector Add(TwoDirectionVector vector)
    {
      DecimalNumber newX = this.X + vector.X;
      DecimalNumber newY = this.Y + vector.Y;
      return new TwoDirectionVector(newX, newY);
    }

    public static TwoDirectionVector operator +(TwoDirectionVector vector1, TwoDirectionVector vector2)
    {
      return vector1.Add(vector2);
    }

    public TwoDirectionVector Subtract(TwoDirectionVector vector)
    {
      DecimalNumber newX = this.X - vector.X;
      DecimalNumber newY = this.Y - vector.Y;
      return new TwoDirectionVector(newX, newY);
    }

    public static TwoDirectionVector operator -(TwoDirectionVector vector1, TwoDirectionVector vector2)
    {
      return vector1.Subtract(vector2);
    }
  }
}
