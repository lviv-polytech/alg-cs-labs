using System;
using System.IO;
using System.Text;

namespace Lpnu.Lb6;

/// <summary>
/// Stream token reader helper for robust whitespace-delimited reading across tabs, spaces, and newlines.
/// </summary>
public static class StreamTokenReader
{
    /// <summary>
    /// Reads the next whitespace-delimited token from the given TextReader.
    /// </summary>
    /// <param name="reader">Input text reader.</param>
    /// <returns>Next string token.</returns>
    /// <exception cref="EndOfStreamException">Thrown if end of stream reached before reading a token.</exception>
    public static string ReadNextToken(TextReader reader)
    {
        ArgumentNullException.ThrowIfNull(reader);
        var ch = reader.Read();
        while (ch != -1 && char.IsWhiteSpace((char)ch))
        {
            ch = reader.Read();
        }
        if (ch == -1)
            throw new EndOfStreamException("Input stream ended before all array elements were read.");

        var sb = new StringBuilder();
        while (ch != -1 && !char.IsWhiteSpace((char)ch))
        {
            sb.Append((char)ch);
            ch = reader.Read();
        }
        return sb.ToString();
    }
}

/// <summary>
/// Iterative implementation of Task 6.3:
/// Array input from stream and formatted printing via ordinary and template methods.
/// </summary>
public static class Task3Iterative
{
    public const int DefaultFieldWidth = 8;

    // =========================================================================
    // Part A: Ordinary Iterative Functions (specialized for int)
    // =========================================================================

    /// <summary>
    /// Reads an array of integers iteratively from an input stream.
    /// </summary>
    public static void InputArray(int[] array, TextReader reader, TextWriter? writer = null)
    {
        ArgumentNullException.ThrowIfNull(array);
        if (array.Length > 0) ArgumentNullException.ThrowIfNull(reader);

        for (var i = 0; i < array.Length; i++)
        {
            writer?.Write($"a[{i}] = ");
            var token = StreamTokenReader.ReadNextToken(reader);
            array[i] = int.Parse(token);
        }
    }

    /// <summary>
    /// Prints an array of integers iteratively in a single formatted row with fixed column width.
    /// </summary>
    public static void PrintArray(int[] array, TextWriter writer, int fieldWidth = DefaultFieldWidth)
    {
        ArgumentNullException.ThrowIfNull(array);
        ArgumentNullException.ThrowIfNull(writer);
        for (var i = 0; i < array.Length; i++)
        {
            writer.Write(array[i].ToString().PadLeft(fieldWidth));
        }
        writer.WriteLine();
    }

    /// <summary>
    /// Formats an array of integers iteratively into a string with fixed column width.
    /// </summary>
    public static string FormatArray(int[]? array, int fieldWidth = DefaultFieldWidth)
    {
        if (array == null) return string.Empty;
        var sb = new StringBuilder();
        for (var i = 0; i < array.Length; i++)
        {
            sb.Append(array[i].ToString().PadLeft(fieldWidth));
        }
        return sb.ToString();
    }

    // =========================================================================
    // Part B: Function Templates (generic for any type T)
    // =========================================================================

    /// <summary>
    /// Generic iterative template function for reading array elements from a stream.
    /// </summary>
    public static void InputArrayTemplate<T>(T[] array, TextReader reader, Func<string, T> parser, TextWriter? writer = null)
    {
        ArgumentNullException.ThrowIfNull(array);
        if (array.Length > 0)
        {
            ArgumentNullException.ThrowIfNull(reader);
            ArgumentNullException.ThrowIfNull(parser);
        }

        for (var i = 0; i < array.Length; i++)
        {
            writer?.Write($"a[{i}] = ");
            var token = StreamTokenReader.ReadNextToken(reader);
            array[i] = parser(token);
        }
    }

    /// <summary>
    /// Generic iterative template function for printing formatted array elements to a writer.
    /// </summary>
    public static void PrintArrayTemplate<T>(T[] array, TextWriter writer, int fieldWidth = DefaultFieldWidth)
    {
        ArgumentNullException.ThrowIfNull(array);
        ArgumentNullException.ThrowIfNull(writer);
        for (var i = 0; i < array.Length; i++)
        {
            var str = array[i]?.ToString() ?? "null";
            writer.Write(str.PadLeft(fieldWidth));
        }
        writer.WriteLine();
    }

    /// <summary>
    /// Generic iterative template function for formatting array elements into a string.
    /// </summary>
    public static string FormatArrayTemplate<T>(T[]? array, int fieldWidth = DefaultFieldWidth)
    {
        if (array == null) return string.Empty;
        var sb = new StringBuilder();
        for (var i = 0; i < array.Length; i++)
        {
            var str = array[i]?.ToString() ?? "null";
            sb.Append(str.PadLeft(fieldWidth));
        }
        return sb.ToString();
    }
}

/// <summary>
/// Recursive implementation of Task 6.3:
/// Array input from stream and formatted printing via ordinary and template methods.
/// </summary>
public static class Task3Recursive
{
    public const int DefaultFieldWidth = 8;

    // =========================================================================
    // Part A: Ordinary Recursive Functions (specialized for int)
    // =========================================================================

    /// <summary>
    /// Reads an array of integers recursively from an input stream.
    /// </summary>
    public static void InputArrayRec(int[] array, TextReader reader, int index = 0, TextWriter? writer = null)
    {
        ArgumentNullException.ThrowIfNull(array);
        if (index >= array.Length) return;
        ArgumentNullException.ThrowIfNull(reader);

        writer?.Write($"a[{index}] = ");
        var token = StreamTokenReader.ReadNextToken(reader);
        array[index] = int.Parse(token);
        InputArrayRec(array, reader, index + 1, writer);
    }

    /// <summary>
    /// Prints an array of integers recursively in a single formatted row with fixed column width.
    /// </summary>
    public static void PrintArrayRec(int[] array, TextWriter writer, int index = 0, int fieldWidth = DefaultFieldWidth)
    {
        ArgumentNullException.ThrowIfNull(array);
        ArgumentNullException.ThrowIfNull(writer);
        if (index >= array.Length)
        {
            writer.WriteLine();
            return;
        }
        writer.Write(array[index].ToString().PadLeft(fieldWidth));
        PrintArrayRec(array, writer, index + 1, fieldWidth);
    }

    /// <summary>
    /// Formats an array of integers recursively into a string with fixed column width.
    /// </summary>
    public static string FormatArrayRec(int[]? array, int index = 0, int fieldWidth = DefaultFieldWidth)
    {
        if (array == null || index >= array.Length) return string.Empty;
        var element = array[index].ToString().PadLeft(fieldWidth);
        return element + FormatArrayRec(array, index + 1, fieldWidth);
    }

    // =========================================================================
    // Part B: Function Templates (generic for any type T)
    // =========================================================================

    /// <summary>
    /// Generic recursive template function for reading array elements from a stream.
    /// </summary>
    public static void InputArrayTemplateRec<T>(T[] array, TextReader reader, Func<string, T> parser, int index = 0, TextWriter? writer = null)
    {
        ArgumentNullException.ThrowIfNull(array);
        if (index >= array.Length) return;
        ArgumentNullException.ThrowIfNull(reader);
        ArgumentNullException.ThrowIfNull(parser);

        writer?.Write($"a[{index}] = ");
        var token = StreamTokenReader.ReadNextToken(reader);
        array[index] = parser(token);
        InputArrayTemplateRec(array, reader, parser, index + 1, writer);
    }

    /// <summary>
    /// Generic recursive template function for printing formatted array elements to a writer.
    /// </summary>
    public static void PrintArrayTemplateRec<T>(T[] array, TextWriter writer, int index = 0, int fieldWidth = DefaultFieldWidth)
    {
        ArgumentNullException.ThrowIfNull(array);
        ArgumentNullException.ThrowIfNull(writer);
        if (index >= array.Length)
        {
            writer.WriteLine();
            return;
        }
        var str = array[index]?.ToString() ?? "null";
        writer.Write(str.PadLeft(fieldWidth));
        PrintArrayTemplateRec(array, writer, index + 1, fieldWidth);
    }

    /// <summary>
    /// Generic recursive template function for formatting array elements into a string.
    /// </summary>
    public static string FormatArrayTemplateRec<T>(T[]? array, int index = 0, int fieldWidth = DefaultFieldWidth)
    {
        if (array == null || index >= array.Length) return string.Empty;
        var str = array[index]?.ToString() ?? "null";
        return str.PadLeft(fieldWidth) + FormatArrayTemplateRec(array, index + 1, fieldWidth);
    }
}
