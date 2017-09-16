using System;
using System.IO;

namespace CL.DAO
{
    /// <summary>
    /// bedoeld om min of meer transparant 
    /// bestanden lokaal of remote te benaderen.
    /// 
    /// voorlopig nog niet gebruikt
    /// is ook nog niet helemaal af !
    /// TODO: merge-gedeelte afwerken (template-pattern)
    /// </summary>
    public abstract class DAOBase
    {

        private DirectoryInfo _currentFolder = null;

        private string _localFolderName;
        private string _remoteFolderName;

        public DAOBase(string profileName, string localFolderName, string remoteFolderName)
        {
            _localFolderName = localFolderName;
            _remoteFolderName = remoteFolderName;

            _currentFolder = GetDataFolder(profileName);
        }

        private DirectoryInfo GetDirectoryInfo(string path)
        {
            var di = new DirectoryInfo(path);
            if (!di.Exists)
                di.Create();

            return di;
        }

        private DirectoryInfo GetDataFolder(string profileName)
        {
            if (_currentFolder == null)
            {
                DirectoryInfo di = null;

                try
                {
                    di = new DirectoryInfo(_remoteFolderName);
                    if (!di.Exists)
                        throw new Exception();

                    MergeLocalAndRemote(profileName);
                }
                catch (Exception)
                {
                    di = GetDirectoryInfo(_localFolderName);
                }

                _currentFolder = GetDirectoryInfo(Path.Combine(di.FullName, profileName));

            }

            return _currentFolder;
        }

        //protected abstract string fileName;
        //protected abstract string MergeData(); //todo: wat moet dit wel zijn ? ( T ? )
        //class generic maken ( T ? )

        private void MergeLocalAndRemote(string profileName)
        {
            //werken met lijst van bestanden, die eventueel in constructor
            //worden meegegeven ? (TODO)

            var localFolder = GetDirectoryInfo(Path.Combine(_localFolderName, profileName));
            string localScoresFilename = Path.Combine(localFolder.FullName, "scores.xml");

            if (File.Exists(localScoresFilename))
            {
                //merge with remote scores
                var remoteFolder = GetDirectoryInfo(Path.Combine(_remoteFolderName, profileName));
                string remoteScoresFilename = Path.Combine(remoteFolder.FullName, "scores.xml");

                //SaveScores(remoteScores, profileName);

                //Save(remoteScoresFilename, remoteScores);
            }

        }

        public void Save<T>(string fileName, string profileName, T data)
        {

            DirectoryInfo profileDi = GetDataFolder(profileName);

            string outputFilename = Path.Combine(profileDi.FullName, fileName);

            Save<T>(outputFilename, data);

        }

        public void Save<T>(string fileName, T data)
        {
            System.Xml.Serialization.XmlSerializer xmlSerializer = new System.Xml.Serialization.XmlSerializer(data.GetType());

            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }

            FileInfo f = new FileInfo(fileName);
            using (FileStream fs = f.OpenWrite())
                xmlSerializer.Serialize(fs, data);

        }

    }
}
