// <file>
//     <copyright see="prj:///doc/copyright.txt"/>
//     <license see="prj:///doc/license.txt"/>
//     <owner name="Daniel Grunwald"/>
//     <version>$Revision$</version>
// </file>

using ICSharpCode.AvalonEdit.Gui;
using System;
using System.Windows;
using System.Windows.Controls;
using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Utils;

namespace ICSharpCode.AvalonEdit.CodeCompletion
{
	/// <summary>
	/// A popup-like window that is attached to a text segment.
	/// </summary>
	public class InsightWindow : CompletionWindowBase
	{
		static InsightWindow()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(InsightWindow),
			                                         new FrameworkPropertyMetadata(typeof(InsightWindow)));
			AllowsTransparencyProperty.OverrideMetadata(typeof(InsightWindow),
			                                            new FrameworkPropertyMetadata(Boxes.True));
		}
		
		/// <summary>
		/// Creates a new InsightWindow.
		/// </summary>
		public InsightWindow(TextArea textArea) : base(textArea)
		{
			this.CloseAutomatically = true;
		}
		
		/// <summary>
		/// Gets/Sets whether the insight window should close automatically.
		/// The default value is true.
		/// </summary>
		public bool CloseAutomatically { get; set; }
		
		/// <inheritdoc/>
		protected override bool CloseOnFocusLost {
			get { return this.CloseAutomatically; }
		}
		
		/// <inheritdoc/>
		protected override void AttachEvents()
		{
			base.AttachEvents();
			this.TextArea.Caret.PositionChanged += CaretPositionChanged;
		}
		
		/// <inheritdoc/>
		protected override void DetachEvents()
		{
			this.TextArea.Caret.PositionChanged -= CaretPositionChanged;
			base.DetachEvents();
		}
		
		void CaretPositionChanged(object sender, EventArgs e)
		{
			if (this.CloseAutomatically) {
				int offset = this.TextArea.Caret.Offset;
				if (offset < this.StartOffset || offset > this.EndOffset) {
					Close();
				}
			}
		}
	}
}