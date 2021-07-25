# Win10 - WebView2 - WPF - Virtual Host Name Path Length Problem Test

Using SetVirtualHostNameToFolderMapping with WebView2 seems like an incredibly useful way to quickly preview and browse local content.

In testing I recently found that it appears that long file names in Windows break the preview.

![Broken Img with long VirtualHostNAmePath](ScreenSnipShowingBrokenImage.JPG)

With GitHub Desktop I had to set git config core.longpaths true to commit.
