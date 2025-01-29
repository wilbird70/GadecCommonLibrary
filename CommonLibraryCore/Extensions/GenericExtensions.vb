'Gadec Engineerings Software (c) 2022
'Common Library
Imports System.Runtime.CompilerServices

Public Module GenericExtensions

    'subs

    ''' <summary>
    ''' Adds the specified key and value to the dictionary if the key not exists.
    ''' </summary>
    ''' <typeparam name="TKey"></typeparam>
    ''' <typeparam name="TValue"></typeparam>
    ''' <param name="eDictionary"></param>
    ''' <param name="key">The key of the element to add.</param>
    ''' <param name="value">The value of the element to add. The value can be null for reference types.</param>
    <Extension()>
    Public Sub TryAdd(Of TKey, TValue)(ByRef eDictionary As Dictionary(Of TKey, TValue), key As TKey, value As TValue)
        If eDictionary.ContainsKey(key) Then Exit Sub

        eDictionary.Add(key, value)
    End Sub

    ''' <summary>
    ''' Adds the keyvaluepairs to the dictionary.
    ''' <para>If AddMode is not specified, existing keys will be ignored (default AddMode.NewOnly).</para>
    ''' <para>With AddMode.Override the value of existing keys will be overridden.</para>
    ''' </summary>
    ''' <typeparam name="TKey"></typeparam>
    ''' <typeparam name="TValue"></typeparam>
    ''' <param name="eDictionary"></param>
    ''' <param name="keyValuePairs">An array of <see cref="KeyValuePair(Of TKey, TValue)"/>.</param>
    ''' <param name="addMode">Optional AddMode value.</param>
    <Extension()>
    Public Sub AddRange(Of TKey, TValue)(ByRef eDictionary As Dictionary(Of TKey, TValue), keyValuePairs As KeyValuePair(Of TKey, TValue)(), Optional addMode As AddMode = AddMode.NewOnly)
        For Each pair In keyValuePairs
            Select Case True
                Case Not eDictionary.ContainsKey(pair.Key) : eDictionary.Add(pair.Key, pair.Value)
                Case addMode = AddMode.Override : eDictionary(pair.Key) = pair.Value
            End Select
        Next
    End Sub

    'functions

    ''' <summary>
    ''' Converts the ienumerable collection to a sorted list.
    ''' </summary>
    ''' <param name="eIEnumerable"></param>
    ''' <returns>The sorted list.</returns>
    <Extension()>
    Public Function ToSortedList(Of TValue)(ByVal eIEnumerable As IEnumerable(Of TValue)) As List(Of TValue)
        Dim output = eIEnumerable.ToList
        output.Sort()
        Return output
    End Function

    ''' <summary>
    ''' Converts the array of ini-file-like stringexpressions (eg 'Baud=14400') to a dictionary.
    ''' </summary>
    ''' <param name="eIEnumerable"></param>
    ''' <returns>The dictionary.</returns>
    <Extension()>
    Public Function ToIniDictionary(ByVal eIEnumerable As IEnumerable(Of String)) As Dictionary(Of String, String)
        Dim output = New Dictionary(Of String, String)
        For Each line In eIEnumerable
            Dim pair = line.Cut("=")
            If output.ContainsKey(pair(0)) Then Continue For

            Select Case pair.Count > 1
                Case True : output.TryAdd(pair(0), pair(1))
                Case Else : output.TryAdd(pair(0), "")
            End Select
        Next
        Return output
    End Function

    ''' <summary>
    ''' Converts the dictionary to an array of ini-file-like stringexpressions (eg 'Baud=14400').
    ''' </summary>
    ''' <param name="eDictionary"></param>
    ''' <returns>The stringarray.</returns>
    <Extension()>
    Public Function ToIniStringArray(ByVal eDictionary As Dictionary(Of String, String)) As String()
        Return eDictionary.Select(Function(pair) "{0}={1}".Compose(pair.Key, pair.Value)).ToArray
    End Function

End Module
