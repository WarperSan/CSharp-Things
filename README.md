# CSharp-Things
Useful commands to use in a C# Console Project FRAMEWORK

# ConsoleUtils
### AskUser\<T>(string message, \[ConsoleColor color])
Displays the given message in the given color and returns the valid user input of the given type.

### ShowConsoleMessage(object message, \[bool useWriteLine], \[ConsoleColor messageColor])
Displays the given message in the given color and skips a line if specified.

### ShowMenu(string[] options, \[bool clearConsole], \[char updownChar], \[char sideChar])
Displays a menu containing the given options and returns the index of the selected option.

# ArrayUtils
### IsSame\<T>(T[] array1, T[] array2)
Checks if the given arrays contains the same items in no particular order.

### IsAbsoluteSame\<T>(T[] array1, T[] array2)
Checks if the given arrays contains the same items in the same order.

### ShowArray\<T>(T[] array, \[string arrayName])
Prints the given array in the console with the given name. (e.g.: "Array: { 1 2 3 }")

### InitiliaseArrayToValue\<T>(T value, int length)
Returns an array of the given length that only contains the given value.

### InterchangeCells\<T>(T[] array, int index1, int index2)
Swaps two given items in the array.

### GetArrayPortion\<T>(T[] array, int startIndex, int length)
Returns the selected portion of the given array.

# NumberUtils
### SimplifyNumber\<T>(T number)
Returns the simplified version of the given number. (1_000 => 1K; 1_234_567 => 1.234M)

### IsInBounds\<T>(T minBound, T maxBound, T number)
Checks if the given number is in the given bounds

### IsPrimeNumber(int number)
Checks if the given number is prime.

### Max\<T>(T obj1, T obj2)
Returns the biggest number between the two given numbers.

### Max\<T>(T[] array)
Returns a KeyValuePair which contains as the key the biggest number in the array and as the value the indexes of the items which are equal to the biggest number.

### Min\<T>(T obj1, T obj2)
Returns the smallest number between the two given numbers.

### Min\<T>(T[] array)
Returns a KeyValuePair which contains as the key the smallest number in the array and as the value the indexes of the items which are equal to the smallest number.

### Sum\<T>(T[] numbers)
Returns the sum of all the items of the given array.

### Average\<T>(T[] numbers)
Returns the average of all the items of the give array.

### RemoveEvenNumbers\<T>(ref T[] array)
Removes all the even numbers in the given array.

### RemoveOddNumbers\<T>(ref T[] array)
Removes all the odd numbers in the given array.

# Vector Utils
### Point (Class)
Holds the X and Y position

### DistanceBetweenTwoPoints(Point point1, Point point2)
Returns the distance between the two given points

### MoveTowardsPoint(Point a, Point b, int distance)
Returns the interpolated point between the points a and b with a maximum movement of distance. 
