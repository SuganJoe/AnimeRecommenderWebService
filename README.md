# AnimeRecommenderWebService
Web Services Final Project
Suganya Jeyaraman, Ramya Kumar, Brandon Olson

Problem 
	Currently there are many streaming sites where users can consume anime such as Crunchyroll, Anime-Planet, Funimation, etc with a numerous number of anime to watch. The current number of “well-known” anime options sitting around 13,000 combined across multiple platforms accounting for overlap (based off of what is cataloged on MyAnimeList database). With so many options it becomes impractical for a user to be able to manually sift through all of the options and find everything they would otherwise enjoy watching. Although the aforementioned anime streaming platforms do have some provided recommendation systems most are not personalized to the current user either using other users’ written recommendations or recommendations based on show popularity. Additionally, in-house recommendation systems normally do not allow a user to modify the recommendation settings in anyway.
  
Solution Summary
	For our final project we propose to make a user personalized web-based anime recommendation system. Users will be able to use the front end of the application to provide their anime watch history and ratings via their MyAnimeList profile username and password. The user will then be able to make requests to the backend SOAP based recommendation service to retrieve a list of recommendations of anime the user has not already seen and are likely to be highly rated by the user. To create the recommendation system our team will be using a content based approach because we have access to a reasonable amount of content but only one user at a time.
  
Web APIs
MyAnimeList API
	MyAnimeList is a website where users are able to keep track of what anime they have watched and it also provides users the ability to leave a rating among other features. Within our application we will be able to use a given user’s MyAnimeList username (and optionally password) to be able to retrieve the user’s anime list using the MyAnimeList REST API.
Link: https://myanimelist.net/modules.php?go=api#animevalues   

Anime News Network (ANN) API
	Is a general anime and manga media platform providing anime related news, information, merchandise, along with many other things. Anime News Network ANN also provides an anime encyclopedia REST API which provides a service to be able to retrieve a list of all of the anime that is stored on the ANN database. Furthermore the API also provides methods to access detailed information regarding any given anime which includes genre, theme, and rating tags along with many other tags.
Link: https://www.animenewsnetwork.com/encyclopedia/api.php 

Front End
The front end of the application will be a web interface where the user will interact with our anime recommendation web service by providing their MyAnimeList username. Once the user has provided their username they will be able to request a recommendation list. Furthermore the user will be able to change settings to refine the list of recommendations that is returned from the web service. For example, the user could define that they only wanted recommendations from the action genre or recommendations that are similar to a particular anime before sending the request to the web service backend. After the backend returns the recommendations they will be displayed to the user and can be filtered or sorted according to the user’s preferences.

Back End
The back end of our application will consist of our core dataset and our SOAP based recommendation web service. The recommendation web service will take a list of user provided anime names, scores of the given anime, a requested number of recommendations, and any other important metadata to return a list of recommendations with predicted user ratings for each of the given anime. At a high level this will be achieved through a process of creating a user profile based of the users watch history then using a comparison measure (such as cosine similarity score)  to compare the user profile to each of the anime items in the database and return a list of the top-n results.

Dataset
To be able to create a content based recommendation system we need to have a large database of anime information. To achieve this goal we are going to use the ANN API to retrieve the list of all the anime in the ANN database which has about 8,425 anime entries. Each anime entry is about 90 - 100 kb in size so the entire database will be about 0.8 GB to 1 GB in total size. The policy for the ANN API allows us to make one request per second with a max batch of 50 anime titles per request. So theoretically to get the raw data for our database it will take about 169 requests or about 2.8 minutes of time. Once all of the raw data is collected we will vectorize the all of the anime data and store it into the final version of the database. The tags that we will use for the anime vectors (or data matrix) will likely include the genre, theme, and rating tags along with possibly the plot summary (which will be turned into a bag of words), release date, publisher, animation studio and other tags.

