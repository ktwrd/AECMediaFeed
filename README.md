Simple-ish .NET 9 WinForms app for viewing live-ish data from the AEC for Elections via the Media Feed.

Hastily put together on the night of the 2025 Australian Federal Election.

![screenshot](https://res.kate.pet/upload/9d9ea65a3858/AECMediaFeed_mkta68wlsJ.png)

## Resources
- [Media Feed - Australian Electoral Commission](https://www.aec.gov.au/media/mediafeed/)
- [Historical Votes Data - Australian Electoral Commission](https://www.aec.gov.au/election/downloads.htm)
- [AEC Media Feed Version 4.4 - User Guide](https://www.aec.gov.au/media/mediafeed/files/media-feed-user-guide-v4-4.pdf) (also in this repo)

To regenerate the C# Classes (from the XSD files);
- Download the latest archive in the `/*/Standard/Preload/` folder.
- Extract the downloaded zip file.
- Copy the contents of the `schema` folder to the folder: `./Source/AECMediaFeed/Schema`.
- Open up the Visual Studio 2022 Command Prompt
- Run `make.bat` inside of the folder: `./Source/AECMediaFeed/Schema`.
- Recompile the project.

~~You can also test the preload data by clicking on "File" then "Open Preload Data" (select the zip file that was downloaded).~~
Feature not implemented yet!

### Dependencies
- `FluentFTP`
- `NeoSmart.PrettySize`
- `ObjectListView` (specifically `ObjectListView.Repack.NET6Plus`)

## Notes
- Viewing Senate Elections is very buggy atm, and I probably won't fix it anytime soon.
- This is not an all-knowing app. I am not a data scientist, nor an election analyst. If you want accurate information, consult the ABC or a reliable news source.
- The app is currently hard-coded to use the 2025 Federal Election Id (which is `31496`).
  - If you want to look at a different election, change the directory used in the `PullLatestData` method (can be found in `MainForm.cs`).
- This app only supports the live/active FTP server provided by the AEC (`mediafeed.aec.gov.au`), and does not currently support the AEC Media Feed Archives (`mediafeedarchive.aec.gov.au`).