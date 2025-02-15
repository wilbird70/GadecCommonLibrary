using GadecFormsLibrary.Extensions;
using System.Data;

namespace GadecFormsLibrary.Extensions;
public static class ToArrayExtensions
{
    public static Control[] ToArray(this Control.ControlCollection eCollection) => eCollection.Cast<Control>().ToArray();
    public static DataGridViewRow[] ToArray(this DataGridViewRowCollection eCollection) => eCollection.Cast<DataGridViewRow>().ToArray();
}
