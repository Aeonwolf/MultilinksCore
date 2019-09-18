# Welcome to Multilinks

It is fair to say that most of us today are connected the cloud (what most people call the internet these days). We are connected 24/7, at home, at work and everywhere in between. However, most of the "things" in the real world in fact are not connected to the cloud and while these things are useful when we explicitly interact with them, they are not so useful when we don't.

But what if we do have the option of interacting with these "things" from anywhere and in real-time?

The goal of Multilinks is to be a community-driven, cloud-based platform aimed at making it easier for you (an application end-user/developer) and everyone to connect and communicate with other applications and Internet of things from anywhere and in real-time.

## The Big Picture

![Multilinks Big Picture](the_big_picture.gif "The big picture")

## How Does Multilinks Works

Simply speaking, the Multilinks platform is a digital postal/parcel delivery system. It's core function is to deliver labeled packages of data from point A to point B via a secure established link on behalf of the sender.

The Multilinks platform does not validate nor does it modify the packaged data in anyway. It is up to the sender to correctly pack and label the data such that the receiver can unpack and process/consume the data.

### Elements of Multilinks

The Multilinks platform can be broken down into 4 concepts:

#### Endpoints

   * An endpoint is a device with a Multilinks client (an application communicating with the Multilinks platform) running.
   * An endpoint's functionalities are defined by the services defined by Multilinks client that is currently running.
   * An endpoint is a protected resource (i.e. authorisation is required to use it's services).

#### Users

   * Registered Multilinks users will be associated with either `Application User` or `Application Developer` role. There will also exist a `Multilinks Administrator` role to manage the platform (e.g. by default a registered user is an app user and an admin can change that user to an app developer).
      + Application User
         - Can create an endpoint by logging in a Multilinks client.
         - Can send link requests to other endpoints.
         - Can accept link requests from other endpoints.
      + Application Developer
         - Is also an application user.
         - Can create new clients and register them with the Multilinks platform.
         - Can add any services registered on the Multilinks platform to clients they created.
         - Can create new services and register them with the Multilinks platform.

#### Links

#### Services

## Who Would Find Multilinks Useful
