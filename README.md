# KkxWebServer
Settings are mainly stored in KkxGlobals.cs file.

# KkxGlobals::PagesDirectoryPath

KkxGlobals::PagesDirectoryPath - used for regular pages. All .html files in the directory are exposed. Sample directory strucuture:

* Pages
  * Page1.html
  * Page2.html
  * NestedFolder
    * Page1.html

Exposed paths (assumed that the host is localhost:6969):

* /Page1
* /Page2
* /NestedFolder/Page1

# KkxGlobals::FragmentDirectoryPath

KkxGlobals::FragmentDirectoryPath - used for ajax requests (e.g fetching main content, etc.). Works the same as KkxGlobals::PagesDirectoryPath. Caution: KkxGlobals::PagesDirectoryPath and KkxGlobals::FragmentDirectoryPath share the same routes:

* Pages
  * Page1.html
  * Page2.html
  * NestedFolder
    * Page1.html

When you add some fragment files, do not name them the same as in Pages directory! This won't cause any errors, and Page priority will be taken:

* Fragments
  * Page1.html
  *  Page2.html

If you fetch /Page1, you get the resource from Pages directory, not from Fragments!

# KkxGlobals::ExtensionsToScan

Collection containing the extensions that the server can work with. Strongly not recommended to remove .html and .htm extensions.
