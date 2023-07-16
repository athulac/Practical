# Chinook

Note:
have to do code optimize and declare sepearate services for user playlist related.

This application is unfinished. Please complete below tasks. Spend max 2 hours.
We would like to have a short written explanation of the changes you made.

1. Move data retrieval methods to separate class / classes (use dependency injection)
Add seperate service with interface, those registered on progrm.cs and then indject in index and used for GetArtists() method.

2. Favorite / unfavorite tracks. An automatic playlist should be created named "My favorite tracks"
This was done

3. The user's playlists should be listed in the left navbar. If a playlist is added (or modified), this should reflect in the left navbar (NavMenu.razor). Preferrably, the left menu should be refreshed without a full page reload.
this was done

4. Add tracks to a playlist (existing or new one). The dialog is already created but not yet finished.
done for new play lists, exist one is has to implement

5. Search for artist name
done

When creating a user account, you will see this:
"This app does not currently have a real email sender registered, see these docs for how to configure a real email sender. Normally this would be emailed: Click here to confirm your account."
After you click 'Click here to confirm your account' you should be able to login.

Please put the code in Github. Please put the original code (our code) in the master branch, put your code in a separate branch, and make a pull request to the master branch.



caathu@gmail.com
Ab@123456


=========================================================================================
Rework summary

1. used seperate services and used DI
2. "My favorite tracks" automatic created and did the add to favorite
3. Play list display in navbar left, modifications show in the nav and its load witout whole page reload.
4. add tracks to playlist popup was done, create new playlist and assing it to user and then assign tracks, modify and add track to exisist playlist.
5. search by name contain done

* code add to git hub, crate development branch from master, working on dev and commit, then make pullrequest and merge it with master.
* comments added for playlist service few methods.

* for this on github: https://github.com/athulac/Practical
* for more on github: https://github.com/athulac
* personal site developed and hosted by myselves, .net core based: http://www.casln.com/
