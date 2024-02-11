Feature: GoogleMap Map Search Addresses

Background:
	Given User is in google map home page

Scenario: Search one address
	When User enters 'Delhi' address in search box
	Then User should see 'Delhi' in result window

Scenario Outline: Switch start and end point
	Given User Click on Direction Button
	And User enter <startPoint> as Start Address
	And User enter <Destination> as End Address
	When User Click reverse address button
	Then <startPoint> and <Destination> should switch

	Examples:
		| startPoint             | Destination |
		| London, United Kingdom | Luton, UK   |

Scenario: Search two addresses then best travel mode should appear
	Given User Click on Direction Button
	And User enter 'Spain' as Start Address
	And User enter 'Portugal' as End Address
	When User Click on search button
	Then User should see best travel modes in result

Scenario: Search wrong address
	When User enters '===' address in search box
	Then User should see 'Google Maps can't find ===' as error message in result window

Scenario: No direction between two very far addresses
	Given User Click on Direction Button
	And User enter 'Sweden' as Start Address
	And User enter 'China' as End Address
	When User Click on search button
	Then User should see error message 'Sorry, we could not calculate directions'