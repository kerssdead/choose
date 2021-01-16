using Options;

namespace Cnsl
{
	partial class Consol
	{
		static public void DownArrow(ref int currentElem, Commander currentDirectory, int currentPage, int pageMax)
		{
			if (currentElem + 1 < currentDirectory.GetSize() && currentElem + 1 < (currentPage + 1) * pageMax)
				currentElem++;
		}
		static public void UpArrow(ref int currentElem, Commander currentDirectory, int currentPage, int pageMax)
		{
			if (currentElem - 1 > -1 && currentElem - 1 >= currentPage * pageMax)
				currentElem--;
		}
		static public void RightArrow(ref int currentPage, ref int currentElem, Commander currentDirectory, int pageMax)
		{
			currentPage = currentDirectory.GetNextPage(currentPage, pageMax);
			currentElem = currentPage * pageMax;
		}
		static public void LeftArrow(ref int currentPage, ref int currentElem, Commander currentDirectory, int pageMax)
		{
			currentPage = currentDirectory.GetPrevPage(currentPage, pageMax);
			currentElem = currentPage * pageMax;
		}
	}
}
