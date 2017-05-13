public static class NumberTools {
	public static int Loop(int value, int max, int min){
		if (value > max) {
			return min;
		}
		return value;
	}
}
