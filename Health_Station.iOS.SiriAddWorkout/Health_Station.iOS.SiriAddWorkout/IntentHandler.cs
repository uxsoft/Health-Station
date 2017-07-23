using System;
using System.Collections.Generic;

using Foundation;
using Intents;
using ObjCRuntime;

namespace Health_Station.iOS.SiriAddWorkout
{
    // As an example, this class is set up to handle Message intents.
    // You will want to replace this or add other intents as appropriate.
    // The intents you wish to handle must be declared in the extension's Info.plist.

    // You can test your example integration by saying things to Siri like:
    // "Send a message using <myApp>"
    // "<myApp> John saying hello"
    // "Search for messages in <myApp>"
    [Register("IntentHandler")]
    public class IntentHandler : INExtension, IINStartWorkoutIntentHandling
    {
        protected IntentHandler(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public override NSObject GetHandler(INIntent intent)
        {
            // This is the default implementation.  If you want different objects to handle different intents,
            // you can override this and return the handler you want for that particular intent.

            return this;
        }

        // Implement resolution methods to provide additional information about your intent (optional).
        [Export("resolveRecipientsForSearchForMessages:withCompletion:")]
        public void ResolveRecipients(INSendMessageIntent intent, Action<INPersonResolutionResult[]> completion)
        {
            var recipients = intent.Recipients;
            // If no recipients were provided we'll need to prompt for a value.
            if (recipients.Length == 0)
            {
                completion(new INPersonResolutionResult[] { INPersonResolutionResult.NeedsValue });
                return;
            }

            var resolutionResults = new List<INPersonResolutionResult>();

            foreach (var recipient in recipients)
            {
                var matchingContacts = new INPerson[] { recipient }; // Implement your contact matching logic here to create an array of matching contacts
                if (matchingContacts.Length > 1)
                {
                    // We need Siri's help to ask user to pick one from the matches.
                    resolutionResults.Add(INPersonResolutionResult.GetDisambiguation(matchingContacts));
                }
                else if (matchingContacts.Length == 1)
                {
                    // We have exactly one matching contact
                    resolutionResults.Add(INPersonResolutionResult.GetSuccess(recipient));
                }
                else
                {
                    // We have no contacts matching the description provided
                    resolutionResults.Add(INPersonResolutionResult.Unsupported);
                }
            }

            completion(resolutionResults.ToArray());
        }

        [Export("resolveContentForSendMessage:withCompletion:")]
        public void ResolveContent(INSendMessageIntent intent, Action<INStringResolutionResult> completion)
        {
            var text = intent.Content;
            if (!string.IsNullOrEmpty(text))
                completion(INStringResolutionResult.GetSuccess(text));
            else
                completion(INStringResolutionResult.NeedsValue);
        }

        // Once resolution is completed, perform validation on the intent and provide confirmation (optional).
        [Export("confirmSendMessage:completion:")]
        public void ConfirmSendMessage(INSendMessageIntent intent, Action<INSendMessageIntentResponse> completion)
        {
            // Verify user is authenticated and your app is ready to send a message.

            var userActivity = new NSUserActivity("INSendMessageIntent");
            var response = new INSendMessageIntentResponse(INSendMessageIntentResponseCode.Ready, userActivity);
            completion(response);
        }

        public void HandleStartWorkout(INStartWorkoutIntent intent, Action<INStartWorkoutIntentResponse> completion)
        {
            throw new NotImplementedException();
        }
    }
}
