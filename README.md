#CenturyLinkCloud-.NET-SDK

##SDK Release Notes

###Overview

The CenturyLink Cloud SDK for .NET is  Portable Class Library designed to provide an easy way to interact with the CenturyLink Cloud API to .NET based applications. It handles all the inner workings of initializing HTTP Requests, serializing and deserializing HTTP Responses into and from strongly typed .NET classes, handling API exceptions etc.

###API Access

The SDK provides access to the following API areas:
* Authentication
* Alerts
* Billing
* Data Centers
* Groups
* Servers

Access to these areas is provided through a Facade class that organizes the above areas into Service class properties. This Facade class is the Client class and it is the main point of interaction between the consumers of the API and the SDK.

###Domain Model

Since the SDK handles all serialization/deserialization operations, it provides access to more than 50 domain classes. This saves time to consumers of the SDK because they no longer have to develop all these classes themselves.

###Role-Based Links
The SDK also simplifies access to certain linked API methods that depend on Role Based Access. If a domain model contains a certain Link to another resource it is extracted into a method as part of the domain model. (Note that only some links - those related to core mobile application features - have been coded. The pattern is there to be replicated for other links as needed.)

###Alert Calculations
The SDK simplifies Alert related operations, since it provides methods that combine several API calls in order to determine if Servers are violating associated Alert Policies. This makes it very easy to retrieve alerts either through the Alert Policies or through individual Servers.

###Simple to Use
The SDK simplifies coding against API endpoints since it encapsulates all the “plumbing-code” and organizes API areas into Service classes. Examples shown below:

###Example 1: Logging In

```
var client = new Client (userName, password);
var authenticationInfo = client.Authentication;
```

The returned object contains the AccountAlias and BearerToken, that will have to be passed through to all subsequent SDK methods to the API.

###Example 2: Get Group Information

```
var client = new Client(authenticationInfo);
 group = await client.Groups.GetGroup(groupId);
```

As shown here, the Client is initialized passing in the authenticationInfo retrieved from the initial call and it makes a call to the Groups Service of the SDK. The GetGroup method returns a .NET class containing all the properties of a group. 

###Example 3: Get Billing Details from Group Link Method

```
var client = new Client(authenticationInfo);
var group = await client.Groups.GetGroup (groupId);
var billingDetails = await client.Billing.GetBillingDetailsFor(group);
```

This example shows how to retrieve a domain model (Group) and then use the linked method to get the group billing details.
