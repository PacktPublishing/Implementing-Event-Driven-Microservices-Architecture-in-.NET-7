# Implementing Event-driven Microservices Architecture in .NET 7 

<a href="https://www.packtpub.com/product/implementing-event-driven-microservices-architecture-in-net-7/9781803232782?utm_source=github&utm_medium=repository&utm_campaign="><img src="https://static.packt-cdn.com/products/9781803232782/cover/smaller" alt="Implementing Event-driven Microservices Architecture in .NET 7 " height="256px" align="right"></a>

This is the code repository for [Implementing Event-driven Microservices Architecture in .NET 7 ](https://www.packtpub.com/product/implementing-event-driven-microservices-architecture-in-net-7/9781803232782?utm_source=github&utm_medium=repository&utm_campaign=), published by Packt.

**Develop event-based distributed applications that can scale with ever-changing business demands using C# 11 and .NET 7**

## What is this book about?
Developers working with event-driven systems will be able to put their knowledge to work with this practical guide to designing and developing event-based microservices. The book provides a hands-on approach to implementation and associated methodologies that will have you up and running, and productive in no time.

This book covers the following exciting features:
* Explore .NET 7 and how it enables the development of applications using EDA
* Understand messaging protocols and producer/consumer patterns and how to implement them in .NET 7
* Test and deploy applications written in .NET 7 and designed using EDA principles
* Account for scaling and resiliency in microservices
* Collect and learn from telemetry at the platform and application level
* Get to grips with the testing and deployment of microservices

If you feel this book is for you, get your [copy](https://www.amazon.com/dp/1803232781) today!

<a href="https://www.packtpub.com/?utm_source=github&utm_medium=banner&utm_campaign=GitHubBanner"><img src="https://raw.githubusercontent.com/PacktPublishing/GitHub/master/GitHub.png" 
alt="https://www.packtpub.com/" border="5" /></a>

## Instructions and Navigations
All of the code is organized into folders. For example, Chapter02.

The code will look like the following:
```
using OpenTelemetry.Metrics;
using OpenTelemetry;
using System.Diagnostics.Metrics;
var meter = new Meter("NameOfMeter");
var counter = meter.CreateCounter<int>(Name: "requestsreceived", Unit: "requests", Description: "Number of
    requests the service receives");
```

**Following is what you need for this book:**
This book will help .NET developers and architects looking to leverage or pivot to microservices while using a domain-driven event model.

With the following software and hardware list you can run all code files present in the book (Chapter 1-14).
### Software and Hardware List
| Chapter | Software required | OS required |
| -------- | ------------------------------------ | ----------------------------------- |
| 1 | Visual Studio / Visual Studio Code | Windows, Mac OS X, and Linux (Any) |
| 1 | .NET 7 | Windows, Mac OS X, and Linux (Any) |
| 7 | Azure Cloud Services | Windows, Mac OS X, and Linux (Any) |
| 10 | Terraform | Windows, Mac OS X, and Linux (Any) |
| 10 | Kubernetes (Cloud-based, Docker Desktop, MiniKube, k3s, | Windows, Mac OS X, and Linux (Any) |
| 1 | Docker | Windows, Mac OS X, and Linux (Any) |
| 1 | Docker Compose | Windows, Mac OS X, and Linux (Any) |

We also provide a PDF file that has color images of the screenshots/diagrams used in this book. [Click here to download it](https://packt.link/C9mhU).

### Related products
* Microservices Design Patterns in .NET  [[Packt]](https://www.packtpub.com/product/microservices-design-patterns-in-net/9781804610305?utm_source=github&utm_medium=repository&utm_campaign=) [[Amazon]](https://www.amazon.com/dp/1804610305)

* Parallel Programming and Concurrency with C# 10 and .NET 6  [[Packt]](https://www.packtpub.com/product/parallel-programming-and-concurrency-with-c-10-and-net-6/9781803243672?_ga=2.153142456.1287344892.1663686483-846744100.1661956291&utm_source=github&utm_medium=repository&utm_campaign=) [[Amazon]](https://www.amazon.com/dp/1803243678)



## Get to Know the Authors
**Omar Dean McIver**
is an MCT (Microsoft Certified Trainer) and has experience of more than 12 years developing enterprise grade applications in Oil & Gas and other regulated industries. He specialises in cloud-native development and application modernization. He is a certified Azure Solution Architect and FinOps Practitioner. His Udemy course on Practical OAuth, OpenID, and JWT in C# .NET Core has a rating of 4.5-stars. Omar continues to stay at the forefront of cloud-native development with a keen focus on cost optimization, performance tuning, and highly scalable microservice architectures.

**Joshua Garverick**
is a Microsoft MVP (Most Valuable Professional) and a seasoned IT professional with more than 15 years of enterprise experience working in several large industries (finance, healthcare, transportation, logistics). He specializes in Application Lifecycle Management and is currently involved with DevOps and architecture projects, focusing specifically on software architecture and enterprise needs. He published a course with us on Mastering Visual Studio 2019 which has received 4.5-star ratings on Udemy. Joshua has been a Visual Studio ALM Ranger, where he provided guidance, practical experience, and solutions to the developer community.





### Download a free PDF

 <i>If you have already purchased a print or Kindle version of this book, you can get a DRM-free PDF version at no cost.<br>Simply click on the link to claim your free PDF.</i>
<p align="center"> <a href="https://packt.link/free-ebook/9781803232782">https://packt.link/free-ebook/9781803232782 </a> </p>