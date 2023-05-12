namespace ConsoleApp.Exercise
{
  public class Exercise7
  {
    private int x;
    private int[] D;
    private int[,] store;

    public Exercise7(int[] D, int x)
    {
      this.x = x;
      this.D = D;
      store = new int[x + 1, D.Length];
    }

    public int Handle()
    {
      return DynamicHandle(this.x, this.D.Length - 1);
    }

    private int DynamicHandle(int x, int i)
    {
      if (x < 0) return Int32.MaxValue;
      if (x == 0) return 0;
      if (i == 0) return Int32.MaxValue;
      int res = Int32.MaxValue;
      res = Math.Min(res, 1 + DynamicHandle(x - this.D[i], i));
      res = Math.Min(res, DynamicHandle(x, i - 1));
      return res;
    }
  }

  public class Exercise8
  {
    private int k;
    private int n;
    private int[] save;
    private string item1;
    private string item2;

    public Exercise8(int k, int n, string item1, string item2)
    {
      this.k = k;
      this.n = n;
      this.save = new int[n];
      this.item1 = item1;
      this.item2 = item2;
      this.CalculationFibonacci();
    }

    private void CalculationFibonacci()
    {
      save[0] = this.item1.Length;
      save[1] = this.item2.Length;
      for (int i = 2; i < this.n; i++)
      {
        save[i] = save[i - 1] + save[i - 2];
      }
    }

    private string DynamicHandle(int k, int n)
    {
      if (k < 0 || k > this.save[n]) return "";
      else if (n == 0) return this.item1[k].ToString();
      else if (n == 1) return this.item2[k].ToString();
      else
      {

        if (k < this.save[n - 2]) return DynamicHandle(k, n - 2);
        else return DynamicHandle(k - this.save[n - 2], n - 1);
      }
    }

    public string Handle()
    {
      return DynamicHandle(this.k - 1, this.n - 1);
    }
  }
}
