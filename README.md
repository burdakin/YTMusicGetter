# YTMusicGetter
A simple app to download liked songs from YT music as mp3 files.

## Prerequisites

Download the liked songs playlist from Google Takeout. [Here is the video of how to use it](https://www.youtube.com/watch?v=pg39GAPRYeU)
<br> Instead of "music-uploads" choose "my-library-songs". Download the archive.

## Usage
 - Place the InitialList.csv file in the "Files".
 - Run the app.
 - Check the "Output" folder where the song are put.
 - Songs that were not downloaded for some reason will be displayed in the "Logs" folder.

## What's used
- TagLibSharp as an ID3 metadata editor.
- yt-dlp console application as a downloader.
- ffmpeg application as an audio converter.

## Known issues
- Skips tracks saying that the link is broken, but it is alright. 

## What to improve
- Change using console app processes to installed dlls for better code control.
- Make a playlist uploader instead of placing a file to the folder.

## Author
[Sergey Burdakin](https://github.com/burdakin)
