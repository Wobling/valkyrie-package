using System.IO;
using NUnit.Framework;
using UnityEngine;
using Valkyrie.Extensions;
using Directory = UnityEngine.Windows.Directory;

namespace Tests
{
	public class DirectoryInfoExtensionTests
	{
		private string _dirOne = $"{Application.dataPath}/DirLevelOne";
		private string _dirTwo = $"{Application.dataPath}/DirLevelOne/DirLevelTwo";
		private string _dirThree = $"{Application.dataPath}/DirLevelOne/DirLevelTwo/DirLevelThree";
		
		[SetUp]
		public void Setup()
		{
			Directory.CreateDirectory(_dirOne);
			Directory.CreateDirectory(_dirTwo);
			Directory.CreateDirectory(_dirThree);
		}

		[TearDown]
		public void TearDown()
		{
			Directory.Delete(_dirThree);
			Directory.Delete(_dirTwo);
			Directory.Delete(_dirOne);
		}
		
		[Test]
		public void IsSubDirectoryOf_DirectoryProvidedIsSubDirectory_DirectoryIsSubDirectory()
		{
			var dirOneInfo = new DirectoryInfo(_dirOne);
			var dirTwoInfo = new DirectoryInfo(_dirTwo);
			Assert.That(dirOneInfo.IsSubDirectoryOf(dirTwoInfo));
		}
		
		[Test]
		public void IsSubDirectoryOf_DirectoryProvidedTwoLevelsDeep_DirectoryIsSubDirectory()
		{
			var dirOneInfo = new DirectoryInfo(_dirOne);
			var dirThreeInfo = new DirectoryInfo(_dirThree);
			
			Assert.That(dirOneInfo.IsSubDirectoryOf(dirThreeInfo));
		}

		[Test]
		public void FindParentDirectoryWithName_FindDirectoryWithNameTwoLevelsDeep_FindsDirectoryWithGivenName()
		{
			var dirTwoInfo = new DirectoryInfo(_dirTwo);

			DirectoryInfo directoryInfo = dirTwoInfo.FindParentDirectoryWithName("DirLevelOne");
			Assert.That(directoryInfo != null);
		}
		
		[Test]
		public void FindParentDirectoryWithName_FindDirectoryWithNameThreeLevelsDeep_FindsDirectoryWithGivenName()
		{
			var dirThreeInfo = new DirectoryInfo(_dirTwo);

			DirectoryInfo directoryInfo = dirThreeInfo.FindParentDirectoryWithName("DirLevelOne");
			Assert.That(directoryInfo != null);
		}
	}
}