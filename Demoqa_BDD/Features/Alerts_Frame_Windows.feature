Feature: Alerts, Frame & Windows

As a User
I want to interact with various UI elements of the application in Alerts, Frame & Windows subsection such as Browser Windows
So that I can ensure that the behaviour of the elements are correct

Background: 
	Given I'm on the Home page of Demoqa 
	And I navigate to the "Alerts, Frame & Windows" section

@BrowserWindows
Scenario Outline: Verify the browser controlling button behaviour
	When I navigate to the "Browser Windows" subsection
	And I press the "<ButtonName>" button
	Then the "<ExpectedMessage>" should be displayed in a "<WindowType>"
	
	Examples: 
		| ButtonName         | ExpectedMessage                                                                                                         | WindowType         |
		| New Tab            | This is a sample page                                                                                                   | New Tab            |
		| New Window         | This is a sample page                                                                                                   | New Window         |
		| New Window Message | Knowledge increases by sharing but not by saving. Please share this website with your friends and in your organization. | New Window Message |
