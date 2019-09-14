# BreachedEmails
Breached emails app with smart cache pattern using Micorsoft Orleans, Azure local storage Blob and Web Api 2

It can host a Silo in console app that contain the grains in cluster for each domain that has a list of emails. With Api we can check if
email is breached or add a new breached email.

Project structure description:

- BreachedEmails.SmartCache.Host project where we will host Silo for all our grains. Silo is using Azure Blob storage for grain persistence.
  We are using the c# 7.0 or above version that supports async Main method.
  
- BreachedEmails.SmartCache.Interfaces project where all of our signatures of methods are written. Here are all the members that needs to be
  implemented later in class that implements interfaces.
  
- BreachedEmails.SmartCache.Grains project contains all the grains that we have in cluster. For each domain with emails that we add we craete 
  a grain with domain string key.
  
- BreachedEmails.SmartCache.Client project is Web Api 2 project that connects to our host project Silo and can add breached email or check
  if email is breached.
