# Flickr API Reader
Temporary repository to store the sample project, which is capable of consuming Flickr API to search for keywords and then extract information about photo/author from the API. 

App is capable of reading the Flickr API for specific keyword(s). It reads, 

 * Search results
 * Photo details
 * Photo owner details
 
For the UI, it uses Adaptive Triggers to adapt the user interface and `GridView` according to the current screen size. 

App is capable of storing the current page information to reload the page upon next start. 

Note: App is **fully responsive**, and has no UI freezing in any page/action in the user interface. A slight delay may be caused by the Visual Studio Debugger, that is not the problem in application itself. App supports incremental loading, and loads 2 versions of images.

 1. Small version in the grid view for resource friendly loading of the images.
 2. Full version of the image in the photo details page.
 
App also does not consume extra resources, and contains implementations for `IDisposable` to properly clean up the resources. 
