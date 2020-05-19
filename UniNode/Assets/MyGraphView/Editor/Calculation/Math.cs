namespace Graph.Math
{
    /*----------------------------------------------------------------*/
    public static class Int
    {
        public static int Add(int left, int right)
        {
            return left + right;
        }

        public static int Subtract(int left, int right)
        {
            return left - right;
        }

        public static int Multiply(int left, int right)
        {
            return left * right;
        }

        public static int Divide(int left, int right)
        {
            return left / right;
        }

        public static int Modulo(int left, int right)
        {
            return left % right;
        }
    }
    /*----------------------------------------------------------------*/
    public static class Float
    {
        public static float Add(float left,float right)
        {
            return left + right;
        }

        public static float Subtract(float left, float right)
        {
            return left - right;
        }

        public static float Multiply(float left, float right)
        {
            return left * right;
        }

        public static float Divide(float left, float right)
        {
            return left / right;
        }

        public static float Modulo(float left, float right)
        {
            return left % right;
        }
    }
    /*----------------------------------------------------------------*/
    public static class String
    {
        public static string Add(string left, string right)
        {
            return left + right;
        }
    }
    /*----------------------------------------------------------------*/
    public static class Bool
    {
        public static bool Add(bool left, bool right)
        {
            return left || right;
        }

        public static bool Multiply(bool left, bool right)
        {
            return left && right;
        }
    }
    /*----------------------------------------------------------------*/
}