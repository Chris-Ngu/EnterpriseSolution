# EnterpriseSolution
Client based implementation of a business solution to keep track and manage co-workers and tasks
 
Currently thinking about porting to fullstack application with web-based interface

Experiental/Dev branch of EnterpriseSolution is Default with the most up-to-date features

Changes will be implemented there before pushed here

I will be uploading the MySQL schema when the final touches are being done, stil WIP (or will upload upon direct request :) )

This program will not be used for commerical purposes. I am creating this to better understand the concepts of programming, such as modular implementation, networking, APIs, and different frameworks that a company would use 

# Experimental ideas in the works
Main screen is using a placeholder atm since this is probably the most interwoven component and relies on other parts to work first

Integrated messanger for group and individuals with filesharing (maybe screen sharing in the future)

Video calls

Email system used for registration, notifications, or any other purposes (Using System.Net.Email libraries)

*MORE IDEAS WILL BE ADDED HERE AND SOME WILL BE DELETED AS THINGS GET IMPLEMENTED*

# Working features

Registration and login with MySQL server using MySQL connector/net 12/18/19

IMessaging with TCP protocol using WCF framework 12/22/19 (Currently running duplex networking server hardcoded as localhost)

Email support using IMAP and Google App Password 12/24/19 (Smiley22's IMAP DLL)

MySQL like UI for managing co-workers and branch information 12/26/19

Group calendar 12/27/19 (ported from existing Google Calendar API assests to CefSharp Chromium browser using TeamUp)

User preferences and cache 12/29/19

# Contribution and future work

If you would like to understand this concept or would like to help in development, please contact me on Discord: GruntyBunty#3342. I have included a QOLFixes.Txt in the repository in case you just want to get your feet wet. There are very small fixes in the program that could be fixed, but I have no time for them currently and the list will go on forever. That's where you could come in: my first priority in the application is to get the modules working so UI is a second priority. If you have any type of UI work or would like to contribute sample artwork, feel free to :)

# Images 
*All images are WIP designs and are based mainly on pre-existing designs. Subject to change*

![image](https://user-images.githubusercontent.com/57853013/71142237-66d55480-21dc-11ea-8ec4-4cb92307fde8.png)

![image](https://user-images.githubusercontent.com/57853013/71293460-132d4d00-233b-11ea-9229-dac078f7285e.png)

![image](https://user-images.githubusercontent.com/57853013/71328941-cdac8380-24e4-11ea-9c01-3149e1cbf997.png)

![image](https://user-images.githubusercontent.com/57853013/71426862-59a1e500-2676-11ea-95f3-1cc84e798558.png)

![image](https://user-images.githubusercontent.com/57853013/71449592-64f81d80-2715-11ea-98d6-1fc64c17b522.png)

![image](https://user-images.githubusercontent.com/57853013/71449742-12b8fb80-2719-11ea-8461-6a19fc47603a.png)

![image](https://user-images.githubusercontent.com/57853013/71531728-9dcef880-28b5-11ea-8535-55a5f1630cfd.png)
