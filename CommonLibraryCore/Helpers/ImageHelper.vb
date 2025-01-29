'Gadec Engineerings Software (c) 2022
'Common Library

''' <summary>
''' Provides methodes for images.
''' </summary>
Public Class ImageHelper

    ''' <summary>
    ''' Copies a screenshot from a specified location and a specified size to the clipboard.
    ''' </summary>
    ''' <param name="location">Location of the screenshot.</param>
    ''' <param name="size">Size of the screenshot.</param>
    Public Shared Sub GetScreenShot(location As Point, size As Size)
        Dim screenShot As Bitmap
        Dim graphics As Graphics
        screenShot = New Bitmap(size.Width, size.Height, Imaging.PixelFormat.Format32bppRgb)
        graphics = Graphics.FromImage(screenShot)
        graphics.CopyFromScreen(location.X, location.Y, 0, 0, size, CopyPixelOperation.SourceCopy)
        Clipboard.SetImage(screenShot)
        Beep()
    End Sub

    ''' <summary>
    ''' Crops the border of the image, leaving the center with a size of the specified width and height.
    ''' </summary>
    ''' <param name="bitmap">Source image.</param>
    ''' <param name="width">The width of the resulting image.</param>
    ''' <param name="height">The height of the resulting image.</param>
    ''' <returns>The cropped image.</returns>
    Public Shared Function CropImage(bitmap As Bitmap, width As Integer, height As Integer) As Bitmap
        Try
            Dim rectangle = New Rectangle((bitmap.Width - width) / 2, (bitmap.Height - height) / 2, width, height)
            Dim output = New Bitmap(rectangle.Width, rectangle.Height)
            Using graphic = Graphics.FromImage(output)
                graphic.DrawImage(bitmap, New Rectangle(0, 0, rectangle.Width, rectangle.Height), rectangle, GraphicsUnit.Pixel)
                bitmap.Dispose()
                Return output
            End Using
        Catch ex As Exception
            ex.AddData("bitmap.Width: {0}".Compose(bitmap?.Width))
            ex.AddData("bitmap.Height: {0}".Compose(bitmap?.Height))
            ex.AddData("width: {0}".Compose(width))
            ex.AddData("height: {0}".Compose(height))
            ex.AddData("bitmap not Nothing: {0}".Compose(NotNothing(bitmap)))
            ex.Rethrow
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Crops the top/bottom or left/right side of the image, depending on the ratio (specified width and height) of the actual picture.
    ''' </summary>
    ''' <param name="bitmap">Source image.</param>
    ''' <param name="width">The width of the actual picture.</param>
    ''' <param name="height">The height of the actual picture.</param>
    ''' <returns>The cropped image.</returns>
    Public Shared Function CropImageRelative(bitmap As Bitmap, width As Double, height As Double) As Bitmap
        Try
            Dim rectangle As Rectangle
            Select Case bitmap.Height / bitmap.Width < height / width
                Case True : Dim newWidth = (bitmap.Height / height * width) + 1 : rectangle = New Rectangle((bitmap.Width - newWidth) / 2, 0, newWidth, bitmap.Height)
                Case Else : Dim newHeight = (bitmap.Width / width * height) + 1 : rectangle = New Rectangle(0, (bitmap.Height - newHeight) / 2, bitmap.Width, newHeight)
            End Select
            Dim output = New Bitmap(rectangle.Width, rectangle.Height)
            Using graphic = Graphics.FromImage(output)
                graphic.DrawImage(bitmap, New Rectangle(0, 0, rectangle.Width, rectangle.Height), rectangle, GraphicsUnit.Pixel)
                bitmap.Dispose()
                Return output
            End Using
        Catch ex As Exception
            ex.AddData("bitmap.Width: {0}".Compose(bitmap?.Width))
            ex.AddData("bitmap.Height: {0}".Compose(bitmap?.Height))
            ex.AddData("width: {0}".Compose(width))
            ex.AddData("height: {0}".Compose(height))
            ex.AddData("bitmap not Nothing: {0}".Compose(NotNothing(bitmap)))
            ex.Rethrow
            Return Nothing
        End Try
    End Function

End Class
