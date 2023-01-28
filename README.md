# CSharp-Things
Useful commands to use in a C# Console Project FRAMEWORK

# ConsoleUtils
### AskUser\<T>(string message, \[ConsoleColor color])
Shows the given message in the given color and returns the valid user input of the given type.

### ShowConsoleMessage(object message, \[bool useWriteLine], \[ConsoleColor messageColor])
Shows the given message in the given color and skips a line if specified.

### ShowMenu(string[] options, \[bool clearConsole], \[char updownChar], \[char sideChar])
Shows a menu with the given options and returns the index of the selected option.

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
