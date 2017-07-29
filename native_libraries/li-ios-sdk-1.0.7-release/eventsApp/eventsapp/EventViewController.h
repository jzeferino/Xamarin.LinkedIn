#import <UIKit/UIKit.h>

@interface EventViewController : UIViewController

@property NSUInteger tabIndex;
@property NSString *eventName;
@property NSString *eventLocationAndDate;
@property NSArray *eventAttendees;
@property (weak, nonatomic) IBOutlet UILabel *eventNameLabel;
@property (weak, nonatomic) IBOutlet UILabel *eventLocationAndDateLabel;

@end
