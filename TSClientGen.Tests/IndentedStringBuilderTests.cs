using System;
using System.Text;
using NUnit.Framework;

namespace TSClientGen.Tests
{
	[TestFixture]
	public class IndentedStringBuilderTests
	{
		[Test]
		public void Initial_indent_level_is_zero()
		{
			Assert.AreEqual(
				"test", 
				new IndentedStringBuilder().Append("test").ToString());			
		}

		[Test]
		public void Indent_and_unindent_calls_are_summed_up()
		{
			Assert.AreEqual(
				"\ttext", 
				new IndentedStringBuilder()
					.Indent().Indent()
					.Unindent()
					.Append("text")
					.ToString());
		}
		
		[Test]
		public void Indenting_is_applied_on_new_line_only()
		{
			Assert.AreEqual(
				"\tsome word" + Environment.NewLine + "\tanother word", 
				new IndentedStringBuilder()
					.Indent()
					.Append("some ").AppendLine("word")
					.Append("another ").Append("word")
					.ToString());
		}

		[Test]
		public void Indent_call_is_prohibited_if_not_on_a_new_line()
		{
			Assert.Throws<InvalidOperationException>(() =>
				new IndentedStringBuilder().Append("text").Indent());
		}

		[Test]
		public void Unindent_call_is_prohibited_if_not_on_a_new_line()
		{
			Assert.Throws<InvalidOperationException>(() =>
				new IndentedStringBuilder().Indent().Append("text").Unindent());
		}
		
		[Test]
		public void Unindent_call_is_prohibited_if_indent_level_is_zero()
		{
			Assert.Throws<InvalidOperationException>(() =>
				new IndentedStringBuilder().Append("text").Unindent());
		}

		[Test]
		public void Appent_text_adds_indenting_to_all_new_lines()
		{
			var text = new StringBuilder().AppendLine("some text").AppendLine("another text").ToString();
			
			Assert.AreEqual(
				"\tsome text" + Environment.NewLine + "\tanother text" + Environment.NewLine,
				new IndentedStringBuilder().Indent().AppendText(text).ToString());
		}
	}
}