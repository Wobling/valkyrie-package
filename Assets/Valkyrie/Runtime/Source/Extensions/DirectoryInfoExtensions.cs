using System.IO;

namespace Valkyrie.Extensions
{
	public static class DirectoryInfoExtensions 
	{
		/// <summary>
		/// Determines whether [is sub directory of] [the specified dir2].
		/// </summary>
		public static bool IsSubDirectoryOf(this DirectoryInfo self, DirectoryInfo other)
		{
			while (other != null) 
			{
				if (other.FullName.TrimEnd('\\') == self.FullName.TrimEnd('\\')) 
					return true;
				other = other.Parent;
			}
			return false;
		}

		/// <summary>
		/// Finds the parent DirectoryInfo with name 'folderName'. Returns null if no parent by that name exists.
		/// </summary>
		public static DirectoryInfo FindParentDirectoryWithName(this DirectoryInfo self, string folderName)
		{
			while (true) 
			{
				if (self.Parent == null) 
					return null;

				if (string.Equals(self.Name, folderName, System.StringComparison.InvariantCultureIgnoreCase)) 
					return self;

				self = self.Parent;
			}
		}
	}
}