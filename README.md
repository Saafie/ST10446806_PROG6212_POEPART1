# ST10446806_PROG6212_POEPART1
1st commit
This is just a LecturerWindow, its the part of the prototype that has buttons to allow for submission and entering hours and uploading documents due to part 1 asking for front end only, theres no functionality yet.

Upload doc button will allow for uploading documents Submit button submits claims from lecturers

My Claims It will contain all the claims the lecturer submitted and the claims will have a status shown of pending, approved by coordinator, rejected by coordinator, approved by manager and rejected by manager with the date it was submited, the claim ID, Amount, hours and date it was submitted.


2nd commit
Roles Window
Allows for the the user to choose between lecturer and coordinator. If lecturer is picked the lecturer window opens, if coordinator is picked the coordinator window opens. its not buttons its more or circles to pick from. It already has a colour skeem which is blue.

Coordinator Window
The coordinator window was added, It allows for coordinators to review the claims and reject or approve the claims.
It displays the ClaimID, amount, hours and the status of the claim. The close button allows for the window to close and show the roles window again.

Lecturer Window changes
Lecturer window allows for the user to press close to go back and press coordinator.

3rd commit
Added Manager window
It is similar to the Coordinator window, it will have the claims displayed where it will be approved or rejected. the claims will only show once the claims were approved or rejected by the coordinator. there are no examples now because a claim wasnt approved or rejected by coordinator, there is also a close button which will lead back to the roles window.

changes to Roles window
a cirlcle was added to be clicked for manager, a method was added so when manager is clicked is will lead to the manager window, the circles also colour was added.

4th commit
Lecturer, coordinator and manager window all got examples like this, the coordinator and manager window just has one example that allows approval or rejection.
Claim ID: 1 | 13h | R4550 | Submission Pending
    
Claim ID: 2 | 2h | R4550 | Rejected by Coordinator

Claim ID: 3 | 2 | R750 | Approved by coordinator
The time lines of the examples arent 100% correct its just to show what would happen if the prototype had functionality.
so the my claims in the lecturer window will give the status update of the claims.

5th and Final Commit
The last changes that were made was to Lecturer, coordinator and manager windows.xaml. it was just adding colour to it. Lecturer windoww = Pink
Coordinator window = blue
Manager window = purple

Lecturer window was changed, the example data is displayed in columns for better reading, so the columns have headings as well. The headings Sumbit and my Claims are in demiBold. Minor changes was made to duplicate errors in example data.

Everything was commited on github. Final prog will be be uploaded as Zipfile and report documents will be added

Part 2 
Commit 1
Add the window1 that allows for a login to occur for different members, Lecturer, Coordinator, Managers to log in and get to their specific window. Also made changes to the role window to allow login window to pop up but each log in area has its own login colour. Lecturer window was update with functionallity allowing lecturers to make claims and it to show in the provided text box. 

How to log in?:
Lecturer LOGIN Details
- Username: lecturer1
- Password: 1234

Coordinator LOGIN Details
- Username: coordinator1
- Password: 1234

Manager LOGIN Details
- Username: manager1
- Password: 1234

Commit 2 
updated the look of coordinatorWindow, and added functionality, which allowed a coordinator to approve or reject lecture claims. The ability to add documents to the claims was added and is required to make a claim. the ability to view a document was added only to the lecturer window. functionality simalr to coordinates were added to the manager window, allowing for the manager to approve or reject claims. Real time updates are made to the lecturer claims, but it requires you to log back into lecture window to see it change.

Commit 3 
Made adjustments to the look of the windows, corrected any closing errors or duplicates that occured. Added the ability to scroll through the documents but cant access it yes. 

Commit 4
allowed managers and coordinators to view the documents. when they approved or rejected claims, showed that the options are grayed out to ensure them the approval process of that claim is finished. Made only approved claims available to the managers. Altered the colours of buttons to that of its window. Added the ability to say your own rates, and have it calculated, and displayed as an amount. 

Commit 5
Added a unit test, and had the document size be restricted, and what type of documents can be uploaded. Had issues with the x button on manger and coordinator window so, removed it to prevent duplicates. Suggest on those pages press log out
