# NLog.ServiceBus

NLog target for ServiceBus for Windows Server (on premise) ... this might also work with Azure,
however this is using the older ServiceBus APIs because they are the only ones that are 
compatible with the on premise ServiceBus.

I am using this, not for logging, rather for automation chaining.
For example:
Have an existing automation application, that sometimes throws an exception because there is missing data in the target system.
Using only NLog, I can configure this existing application (without any code modifications to the existing system) to now notify our other automation systems that this data is now needed.
