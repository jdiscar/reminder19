// DETAILS : DialogBoxes centered into the parent owner.
//           This class implement the following objects:
//
//   DlgBox.ShowDialog(...)		for CommonDialog and Form
//   MsgBox.Show(...)			for standard MessageBox
//   AppBox.Show(...)			for standard MessageBox with ProductName as caption
//	 ErrBox.Show(...)			for standard error MessageBox
//
//-----------------------------------------------------------------------------
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;

using Reminder19.src.cntrmsgbox.Win32API;
using Reminder19.src.cntrmsgbox.Win32API.Hook;

namespace Reminder19.src.cntrmsgbox.Dialog
{

	///////////////////////////////////////////////////////////////////////
	#region MsgBox

	/// <summary>
	/// Class to display a MessageBox centered on the owner.
	/// </summary>
	/// <remarks>
	/// Same methods as the standard MessageBox.
	/// </remarks>
	/// <example>
	/// This example display a "Hello" message box centered on the owner.
	/// <code>
	/// MsgBox.Show("Hello");
	/// </code>
	/// </example>
	public sealed class MsgBox
	{
		private MsgBox() {}	// To remove the constructor from the documentation!

		///////////////////////////////////////////////////////////////////////
		// text

		/// <summary>
		/// See MSDN MessageBox() method.
		/// </summary>
		public static DialogResult Show(string text)
		{
			CenterWindow centerWindow = new CenterWindow(IntPtr.Zero);
			string caption = Application.ProductName;
			DialogResult dlgResult = MessageBox.Show(text, caption);
			centerWindow.Dispose();
			return dlgResult;
		}

		/// <summary>
		/// See MSDN MessageBox() method.
		/// </summary>
		public static DialogResult Show(IWin32Window owner, string text)
		{
			IntPtr handle = (owner == null) ? IntPtr.Zero: owner.Handle;
			CenterWindow centerWindow = new CenterWindow(handle);
			string caption = Application.ProductName;
			DialogResult dlgResult = MessageBox.Show(owner, text, caption);
			centerWindow.Dispose();
			return dlgResult;
		}

		///////////////////////////////////////////////////////////////////////
		// text, caption

		/// <summary>
		/// See MSDN MessageBox() method.
		/// </summary>
		public static DialogResult Show(string text, string caption)
		{
			CenterWindow centerWindow = new CenterWindow(IntPtr.Zero);
			DialogResult dlgResult = MessageBox.Show(text, caption);
			centerWindow.Dispose();
			return dlgResult;
		}

		/// <summary>
		/// See MSDN MessageBox() method.
		/// </summary>
		public static DialogResult Show(IWin32Window owner, string text, string caption)
		{
			IntPtr handle = (owner == null) ? IntPtr.Zero: owner.Handle;
			CenterWindow centerWindow = new CenterWindow(handle);
			DialogResult dlgResult = MessageBox.Show(owner, text, caption);
			centerWindow.Dispose();
			return dlgResult;
		}

		///////////////////////////////////////////////////////////////////////
		// text, caption, buttons

		/// <summary>
		/// See MSDN MessageBox() method.
		/// </summary>
		public static DialogResult Show(string text, string caption, MessageBoxButtons buttons)
		{
			CenterWindow centerWindow = new CenterWindow(IntPtr.Zero);
			DialogResult dlgResult = MessageBox.Show(text, caption, buttons);
			centerWindow.Dispose();
			return dlgResult;
		}

		/// <summary>
		/// See MSDN MessageBox() method.
		/// </summary>
		public static DialogResult Show(IWin32Window owner, string text, string caption, MessageBoxButtons buttons)
		{
			IntPtr handle = (owner == null) ? IntPtr.Zero: owner.Handle;
			CenterWindow centerWindow = new CenterWindow(handle);
			DialogResult dlgResult = MessageBox.Show(owner, text, caption, buttons);
			centerWindow.Dispose();
			return dlgResult;
		}

		///////////////////////////////////////////////////////////////////////
		// text, caption, buttons, defaultButton

		/// <summary>
		/// See MSDN MessageBox() method.
		/// </summary>
		public static DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
		{
			CenterWindow centerWindow = new CenterWindow(IntPtr.Zero);
			DialogResult dlgResult = MessageBox.Show(text, caption, buttons, icon);
			centerWindow.Dispose();
			return dlgResult;
		}

        public static DialogResult Show(bool center, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            if (!center)
            {
                DialogResult dResult = MessageBox.Show(text, caption, buttons, icon);
                return dResult;
            }
            CenterWindow centerWindow = new CenterWindow(IntPtr.Zero);
            DialogResult dlgResult = MessageBox.Show(text, caption, buttons, icon);
            centerWindow.Dispose();
            return dlgResult;
        }

		/// <summary>
		/// See MSDN MessageBox() method.
		/// </summary>
		public static DialogResult Show(IWin32Window owner, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
		{
			IntPtr handle = (owner == null) ? IntPtr.Zero: owner.Handle;
			CenterWindow centerWindow = new CenterWindow(handle);
			DialogResult dlgResult = MessageBox.Show(owner, text, caption, buttons, icon);
			centerWindow.Dispose();
			return dlgResult;
		}

		///////////////////////////////////////////////////////////////////////
		// text, caption, buttons, defaultButton, icon

		/// <summary>
		/// See MSDN MessageBox() method.
		/// </summary>
		public static DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton)
		{
			CenterWindow centerWindow = new CenterWindow(IntPtr.Zero);
			DialogResult dlgResult = MessageBox.Show(text, caption, buttons, icon, defaultButton);
			centerWindow.Dispose();
			return dlgResult;
		}

		/// <summary>
		/// See MSDN MessageBox() method.
		/// </summary>
		public static DialogResult Show(IWin32Window owner, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton)
		{
			IntPtr handle = (owner == null) ? IntPtr.Zero: owner.Handle;
			CenterWindow centerWindow = new CenterWindow(handle);
			DialogResult dlgResult = MessageBox.Show(owner, text, caption, buttons, icon, defaultButton);
			centerWindow.Dispose();
			return dlgResult;
		}

		///////////////////////////////////////////////////////////////////////
		// text, caption, buttons, defaultButton, icon, options

		/// <summary>
		/// See MSDN MessageBox() method.
		/// </summary>
		public static DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton, MessageBoxOptions options)
		{
			CenterWindow centerWindow = new CenterWindow(IntPtr.Zero);
			DialogResult dlgResult = MessageBox.Show(text, caption, buttons, icon, defaultButton, options);
			centerWindow.Dispose();
			return dlgResult;
		}

		/// <summary>
		/// See MSDN MessageBox() method.
		/// </summary>
		public static DialogResult Show(IWin32Window owner, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton, MessageBoxOptions options)
		{
			IntPtr handle = (owner == null) ? IntPtr.Zero: owner.Handle;
			CenterWindow centerWindow = new CenterWindow(handle);
			DialogResult dlgResult = MessageBox.Show(owner, text, caption, buttons, icon, defaultButton, options);
			centerWindow.Dispose();
			return dlgResult;
		}
	}

	#endregion

	///////////////////////////////////////////////////////////////////////
	#region CenterWindow class

	internal sealed class CenterWindow
	{
		public IntPtr hOwner = IntPtr.Zero;
		private Rectangle rect;

		public CbtHook cbtHook = null;
		public WndProcRetHook wndProcRetHook = null;

		public CenterWindow(IntPtr hOwner)
		{
			this.hOwner = hOwner;
			this.cbtHook = new CbtHook();
			cbtHook.WindowActivate += new CbtHook.CbtEventHandler(WndActivate);
			cbtHook.Install();
		}

		public void Dispose()
		{
			if (wndProcRetHook != null)
			{
				wndProcRetHook.Uninstall();
				wndProcRetHook = null;
			}
			if (cbtHook != null)
			{
				cbtHook.Uninstall();
				cbtHook = null;
			}
		}

		public void WndActivate(object sender, CbtEventArgs e)
		{
			IntPtr hMsgBox = e.wParam;

			// try to find a howner for this message box
			if (hOwner == IntPtr.Zero)
				hOwner = USER32.GetActiveWindow();

			// get the MessageBox window rect
			RECT rectDlg = new RECT();
			USER32.GetWindowRect(hMsgBox, ref rectDlg);

			// get the owner window rect
			RECT rectForm = new RECT();
			USER32.GetWindowRect(hOwner, ref rectForm);

			// get the biggest screen area
			Rectangle rectScreen = API.TrueScreenRect;

			// if no parent window, center on the primary screen
			if (rectForm.right == rectForm.left)
				rectForm.right = rectForm.left = Screen.PrimaryScreen.WorkingArea.Width / 2;
			if (rectForm.bottom == rectForm.top)
				rectForm.bottom = rectForm.top = Screen.PrimaryScreen.WorkingArea.Height / 2;

			// center on parent
			int dx = ((rectDlg.left + rectDlg.right) - (rectForm.left + rectForm.right)) / 2;
			int dy = ((rectDlg.top + rectDlg.bottom) - (rectForm.top + rectForm.bottom)) / 2;

			rect = new Rectangle(
				rectDlg.left - dx,
				rectDlg.top - dy,
				rectDlg.right - rectDlg.left,
				rectDlg.bottom - rectDlg.top);

			// place in the screen
			if (rect.Right > rectScreen.Right) rect.Offset(rectScreen.Right - rect.Right, 0);
			if (rect.Bottom > rectScreen.Bottom) rect.Offset(0, rectScreen.Bottom - rect.Bottom);
			if (rect.Left < rectScreen.Left) rect.Offset(rectScreen.Left - rect.Left, 0);
			if (rect.Top < rectScreen.Top) rect.Offset(0, rectScreen.Top - rect.Top);

			if (e.IsDialog)
			{
				// do the job when the WM_INITDIALOG message returns
				wndProcRetHook = new WndProcRetHook(hMsgBox);
				wndProcRetHook.WndProcRet += new WndProcRetHook.WndProcEventHandler(WndProcRet);
				wndProcRetHook.Install();
			}
			else
				USER32.MoveWindow(hMsgBox, rect.Left, rect.Top, rect.Width, rect.Height, 1);

			// uninstall this hook
			WindowsHook wndHook = (WindowsHook)sender;
			Debug.Assert(cbtHook == wndHook);
			cbtHook.Uninstall();
			cbtHook = null;
		}

		public void WndProcRet(object sender, WndProcRetEventArgs e)
		{
			if (e.cw.message == WndMessage.WM_INITDIALOG ||
				e.cw.message == WndMessage.WM_UNKNOWINIT)
			{
				USER32.MoveWindow(e.cw.hwnd, rect.Left, rect.Top, rect.Width, rect.Height, 1);
				
				// uninstall this hook
				WindowsHook wndHook = (WindowsHook)sender;
				Debug.Assert(wndProcRetHook == wndHook);
				wndProcRetHook.Uninstall();
				wndProcRetHook = null;
			}
		}
	}
	#endregion
}
