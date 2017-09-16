using System;
using System.Collections.Generic;
using System.Linq;
using CL.Entity;
using System.IO;

namespace CL.DAO
{
    public class ProfileDAO
    {
        private DirectoryInfo _currentFolder = null;

        //TODO: _remoteProfileFolder moet in settings !!
        private const string RemoteProfileFolder = @"\\mybookworld\PUBLIC\KIDS\ProfileStoreForAppelsienen";

        private ProfileDAO()
        {
        }

        private static ProfileDAO _instance;
        public static ProfileDAO Instance => _instance ?? (_instance = new ProfileDAO());

        private DirectoryInfo GetDirectoryInfo(string path)
        {
            var di = new DirectoryInfo(path);
            if (!di.Exists)
                di.Create();

            return di;
        }

        private static DirectoryInfo ApplicationData => new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));

        private static string GetProfileFolderName(string profileName)
        {
            return Path.Combine(Path.Combine(ApplicationData.FullName, "Appelsienen"), profileName);
        }

        private DirectoryInfo GetScoresFolder(string profileName)
        {
            if (_currentFolder != null) return _currentFolder;

            DirectoryInfo di = null;

            di = new DirectoryInfo(RemoteProfileFolder);
            if (di.Exists)
            {
                MergeLocalAndRemote(profileName);
            }
            else
            {
                di = GetDirectoryInfo(Path.Combine(ApplicationData.FullName, "Appelsienen"));
            }

            _currentFolder = GetDirectoryInfo(Path.Combine(di.FullName, profileName));

            return _currentFolder;
        }

        private void MergeLocalAndRemote(string profileName)
        {
            var localFolder = GetDirectoryInfo(Path.Combine(Path.Combine(ApplicationData.FullName, "Appelsienen"), profileName));
            string localScoresFilename = Path.Combine(localFolder.FullName, "scores.xml");

            if (File.Exists(localScoresFilename))
            {
                //merge with remote scores
                var remoteFolder = GetDirectoryInfo(Path.Combine(RemoteProfileFolder, profileName));
                string remoteScoresFilename = Path.Combine(remoteFolder.FullName, "scores.xml");

                var localScores = GetScoresByFilename(localScoresFilename);
                var localCopyCatScores = localScores.FirstOrDefault(x => x.Name == "CopyCat");
                var localRecogniseNumbersScores = localScores.FirstOrDefault(x => x.Name == "RecogniseNumbers");
                var localCountOrangesScores = localScores.FirstOrDefault(x => x.Name == "CountOranges");

                var remoteScores = GetScoresByFilename(remoteScoresFilename);
                var remoteCopyCatScores = remoteScores.FirstOrDefault(x => x.Name == "CopyCat");
                var remoteRecogniseNumbersScores = remoteScores.FirstOrDefault(x => x.Name == "RecogniseNumbers");
                var remoteCountOrangesScores = remoteScores.FirstOrDefault(x => x.Name == "CountOranges");

                if (!File.Exists(remoteScoresFilename))
                {
                    remoteCopyCatScores = new Game();
                    remoteRecogniseNumbersScores = new Game();
                    remoteCountOrangesScores = new Game();
                }
                remoteCopyCatScores.Scores.AddRange(localCopyCatScores.Scores);
                remoteCopyCatScores.Scores = remoteCopyCatScores.Scores.OrderBy(x => x.Tijdstip).ToList();

                remoteRecogniseNumbersScores.Scores.AddRange(localRecogniseNumbersScores.Scores);
                remoteRecogniseNumbersScores.Scores = remoteRecogniseNumbersScores.Scores.OrderBy(x => x.Tijdstip).ToList();

                remoteCountOrangesScores.Scores.AddRange(localCountOrangesScores.Scores);
                remoteCountOrangesScores.Scores = remoteCountOrangesScores.Scores.OrderBy(x => x.Tijdstip).ToList();

                File.Delete(localScoresFilename);
                File.Delete(remoteScoresFilename);

                remoteScores = new GamesList
                {
                    remoteCopyCatScores,
                    remoteRecogniseNumbersScores,
                    remoteCountOrangesScores
                };
                
                SaveScoresToFile(remoteScores, remoteScoresFilename);
            }

        }

        /// <summary>
        /// enkel namen en avatars/foto's
        /// </summary>
        /// <returns></returns>
        public List<Profile> GetProfiles()
        {
            var di = GetDirectoryInfo(Path.Combine(ApplicationData.FullName, "Appelsienen"));

            return di.GetDirectories().Select(folder => GetProfile(folder.Name)).ToList();
        }

        /// <summary>
        /// basisdata profiel ophalen
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public Profile GetProfile(string username)
        {
            var profileDi = new DirectoryInfo(GetProfileFolderName(username));

            var xmlSerializer = new System.Xml.Serialization.XmlSerializer(typeof(Profile));

            var f = new FileInfo(Path.Combine(profileDi.FullName, "profile.xml"));
            using (var fs = f.OpenRead())
                return (Profile)xmlSerializer.Deserialize(fs);
        }

        /// <summary>
        /// scores ophalen
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public GamesList GetScoresByUsername(string username)
        {
            var profileDi = GetScoresFolder(username);

            var scoresFilename = Path.Combine(profileDi.FullName, "scores.xml");

            return GetScoresByFilename(scoresFilename);
        }

        public GamesList GetScoresByFilename(string filename)
        {
            System.Xml.Serialization.XmlSerializer xmlSerializer = new System.Xml.Serialization.XmlSerializer((new GamesList()).GetType());

            if (!File.Exists(filename))
                return new GamesList();

            FileInfo f = new FileInfo(filename);
            using (FileStream fs = f.OpenRead())
                return (GamesList)xmlSerializer.Deserialize(fs);
        }

        public void DeleteProfile(Profile profile)
        {
            var profileFoldername = GetProfileFolderName(profile.Name);
            var profileFilename = Path.Combine(profileFoldername, "scores.xml");
            //todo: remote profile verwijderen !!

            File.Delete(profileFilename);
            Directory.Delete(profileFoldername);
        }

        public bool SaveNewProfile(Profile profile)
        {
            try
            {
                if (profile.Name == "")
                {
                    return false;
                }

                var profileFolder = new DirectoryInfo(GetProfileFolderName(profile.Name));
                if (!profileFolder.Exists)
                    profileFolder.Create();

                var profileFilename = Path.Combine(GetProfileFolderName(profile.Name), "profile.xml");

                if (File.Exists(profileFilename))
                {
                    return false;
                }
                else
                {
                    SaveProfile(profile);
                    return true;
                }
            }
            catch (Exception e)
            {
                throw new IOException("Er was een probleem om een nieuw profiel aan te maken op het pad '" + GetProfileFolderName(profile.Name) + "'", e);
            }
        }

        public void SaveScoresToProfile(GamesList gamesList, string profileName)
        {
            var profileDi = GetScoresFolder(profileName);

            var scoresFilename = Path.Combine(profileDi.FullName, "scores.xml");

            SaveScoresToFile(gamesList, scoresFilename);

        }

        public void SaveScoresToFile(GamesList gamesList, string scoresFilename)
        {
            System.Xml.Serialization.XmlSerializer xmlSerializer = new System.Xml.Serialization.XmlSerializer(gamesList.GetType());

            if (File.Exists(scoresFilename))
            {
                File.Delete(scoresFilename);
            }

            FileInfo f = new FileInfo(scoresFilename);
            using (FileStream fs = f.OpenWrite())
                xmlSerializer.Serialize(fs, gamesList);

        }

        public void SaveProfile(Profile fullProfile)
        {
            var bareProfile = new Profile
            {
                Image = fullProfile.Image,
                Name = fullProfile.Name,
                Role = fullProfile.Role,
                CountOrangesMaxValue = fullProfile.CountOrangesMaxValue,
                CountOrangesExcludedValues= fullProfile.CountOrangesExcludedValues,
                RecogniseNumbersMaxValue = fullProfile.RecogniseNumbersMaxValue,
                RecogniseNumbersExcludedValues = fullProfile.RecogniseNumbersExcludedValues
            };
            
            var xmlSerializer = new System.Xml.Serialization.XmlSerializer(bareProfile.GetType());

            var profileFilename = Path.Combine(GetProfileFolderName(bareProfile.Name), "profile.xml");

            if (File.Exists(profileFilename))
            {
                File.Delete(profileFilename);
            }

            var f = new FileInfo(profileFilename);
            using (var fs = f.OpenWrite())
                xmlSerializer.Serialize(fs, bareProfile);

        }

    }
}
