#import <UIKit/UIKit.h>
#import <Foundation/Foundation.h>
#import "EventViewController.h"
#import <linkedin-sdk/LISDK.h>
#import "AttendeeCell.h"
#import "Person.h"
#import "ProfileViewController.h"
#import "HostViewController.h"

#define COLOR_RED [UIColor colorWithRed:0.945 green:0.318 blue:0.325 alpha:1]

@interface EventViewController () <UITableViewDelegate, UITableViewDataSource>
@property (nonatomic, weak) IBOutlet UIView *loginOrAttendView;
@property (nonatomic, weak) IBOutlet UITableView *attendees;
@end

@implementation EventViewController

- (void)viewDidLoad {
    
    [super viewDidLoad];
    self.view.backgroundColor = COLOR_RED;

    _eventNameLabel.text = _eventName;
    [_eventNameLabel setBackgroundColor:COLOR_RED];

    _eventLocationAndDateLabel.text = _eventLocationAndDate;
    [_eventLocationAndDateLabel setBackgroundColor:COLOR_RED];

    BOOL validSession = [LISDKSessionManager hasValidSession];
    UIView *myView = nil;
    if (validSession) {
        NSArray *nibContents = [[NSBundle mainBundle] loadNibNamed:@"AttendView"
                                                             owner:self
                                                           options:nil];
        myView = [nibContents objectAtIndex:0];
    } else {
        NSArray *nibContents = [[NSBundle mainBundle] loadNibNamed:@"LoginView"
                                                             owner:self
                                                           options:nil];
        myView = [nibContents objectAtIndex:0];
    }
    myView.bounds = self.loginOrAttendView.bounds;
    [self.loginOrAttendView addSubview:myView];

    // set configurations for attendee list
    [self.attendees setDataSource:self];
    [self.attendees setDelegate:self];
    self.attendees.separatorStyle = UITableViewCellSeparatorStyleNone;
    self.attendees.userInteractionEnabled = YES;
    self.attendees.scrollEnabled = YES;
    self.attendees.alwaysBounceVertical = YES;
    self.attendees.backgroundColor = COLOR_RED;
}

- (void)didReceiveMemoryWarning {
    [super didReceiveMemoryWarning];
    // Dispose of any resources that can be recreated.
}

- (IBAction)connectWithLinkedIn:(id)sender {
    NSArray *permissions = [NSArray arrayWithObjects:LISDK_BASIC_PROFILE_PERMISSION, LISDK_EMAILADDRESS_PERMISSION, nil];
    [LISDKSessionManager createSessionWithAuth:permissions
                                         state:[NSString stringWithFormat:@"%lu",self.tabIndex]
                        showGoToAppStoreDialog:YES
                                  successBlock:^(NSString *returnState) {
                                      NSLog(@"returned state %@",returnState);
                                  }
                                    errorBlock:^(NSError *error) {
                                        NSLog(@"%s %@","error called! ", [error description]);
                                    }
     ];
}

# pragma mark - table view methods

- (NSInteger)numberOfSectionsInTableView:(UITableView *)tableView
{
    return [self.eventAttendees count];
}

- (NSInteger)tableView:(UITableView *)tableView numberOfRowsInSection:(NSInteger)section
{
    return 1;
}

// Customize the appearance of table view cells.
- (UITableViewCell *)tableView:(UITableView *)tableView cellForRowAtIndexPath:(NSIndexPath *)indexPath
{
    static NSString *CellIdentifier = @"Cell";
    
    Person *person = [self.eventAttendees objectAtIndex:(indexPath.section*1 + indexPath.row)];
    NSString *personName = [[NSString alloc] initWithFormat:@"%@ %@",person.firstName,person.lastName];
    __block UIImage *personImage = [UIImage imageNamed:[[NSBundle mainBundle] pathForResource:@"ghost_person" ofType:@"png"]];

    AttendeeCell *cell = [tableView dequeueReusableCellWithIdentifier:CellIdentifier];
    if (cell == nil) {
        cell = [[AttendeeCell alloc] initWithStyle:UITableViewCellStyleSubtitle reuseIdentifier:CellIdentifier linkedinId:person.linkedinId name:personName headline:person.headline image:personImage];
    }

    if ([LISDKSessionManager hasValidSession]) {
        [[LISDKAPIHelper sharedInstance] apiRequest:[[NSString alloc] initWithFormat:@"https://www.linkedin.com/v1/people/%@:(picture-url)",person.linkedinId]
                                             method:@"GET"
                                               body:nil
                                            success:^(LISDKAPIResponse *response) {
                                                NSError *parseError = nil;
                                                NSDictionary *jsonData = [NSJSONSerialization JSONObjectWithData:[response.data dataUsingEncoding:NSUTF8StringEncoding] options:0 error:&parseError];
                                                if (!parseError) {
                                                    NSString *imageUrl = [jsonData valueForKey:@"pictureUrl"];
                                                    NSURL *url = [NSURL URLWithString:imageUrl];
                                                    NSMutableURLRequest *request = [NSMutableURLRequest requestWithURL:url];
                                                    [NSURLConnection sendAsynchronousRequest:request
                                                                                       queue:[NSOperationQueue mainQueue]
                                                                           completionHandler:^(NSURLResponse *response, NSData *data, NSError *error) {
                                                                               if (!error)
                                                                               {
                                                                                   personImage = [[UIImage alloc] initWithData:data];
                                                                                   cell.personImage = personImage;
                                                                                   cell.imageView.image = cell.personImage;
                                                                               }
                                                                               else
                                                                               {
                                                                                   NSLog(@"Error %@",[error description]);
                                                                               }
                                                                           }];
                                                } else {
                                                    NSLog(@"parse error %@", parseError);
                                                }
                                            }
                                              error:^(LISDKAPIError *apiError) {
                                                  NSInteger errorCode = apiError.code;
                                                  if (errorCode == 401) {
                                                      [LISDKSessionManager clearSession];
                                                  }
                                              }];
    }
    
    // Set up the cell...
    cell.textLabel.font = [UIFont fontWithName:@"HelveticaNeue" size:14.0f];
    cell.textLabel.text = cell.personName;
    cell.textLabel.textAlignment = NSTextAlignmentLeft;
    cell.textLabel.textColor = [UIColor colorWithRed:0.263 green:0.275 blue:0.286 alpha:1];
    cell.detailTextLabel.text = cell.personHeadline;
    cell.detailTextLabel.textAlignment = NSTextAlignmentLeft;
    cell.detailTextLabel.textColor = [UIColor colorWithRed:0.263 green:0.275 blue:0.286 alpha:1];
    cell.imageView.image = cell.personImage;
    cell.backgroundColor = COLOR_RED;
    cell.userInteractionEnabled = YES;
    return cell;
}

- (CGFloat)tableView:(UITableView*)tableView heightForHeaderInSection:(NSInteger)section
{
    return 0.0f;
}

- (CGFloat)tableView:(UITableView*)tableView heightForFooterInSection:(NSInteger)section
{
    return 14.0f;
}

-(UIView*)tableView:(UITableView *)tableView viewForFooterInSection:(NSInteger)section
{
    // fill the section footer with an empty view, effectively introducing spacing between rows.
    UIView *view = [[UIView alloc] init];
    view.backgroundColor = COLOR_RED;
    return view;
}

- (void)tableView:(UITableView *)tableView didSelectRowAtIndexPath:(NSIndexPath *)indexPath {
    if ([LISDKSessionManager hasValidSession]) {
        AttendeeCell *attendee = (AttendeeCell *)[tableView cellForRowAtIndexPath:indexPath];
        
        NSArray *windows = [[UIApplication sharedApplication] windows];
        UIViewController *topController = (windows.count > 0) ? [[windows objectAtIndex:0] rootViewController] : nil;
        while (topController.presentedViewController) {
            topController = topController.presentedViewController;
        }
        if (topController) {
            NSString *requestUrl = [[NSString alloc] initWithFormat:@"https://www.linkedin.com/v1/people/%@:(picture-url)", attendee.linkedinId];
            [[LISDKAPIHelper sharedInstance] apiRequest:requestUrl
                                                 method:@"GET"
                                                   body:nil
                                                success:^(LISDKAPIResponse *response) {
                                                    NSError *parseError = nil;
                                                    NSDictionary *jsonData = [NSJSONSerialization JSONObjectWithData:[response.data dataUsingEncoding:NSUTF8StringEncoding] options:0 error:&parseError];

                                                    ProfileViewController *vc = [[ProfileViewController alloc] initWithNibName:@"ProfileViewController" bundle:[NSBundle mainBundle]];
                                                    vc.linkedinId = attendee.linkedinId;
                                                    vc.name = attendee.personName;
                                                    vc.headline = attendee.personHeadline;
                                                    if (!parseError) {
                                                        vc.imageUrl = [jsonData valueForKey:@"pictureUrl"];
                                                    }
                                                    vc.view.backgroundColor = COLOR_RED;
                                                    UINavigationController *navController = [[UINavigationController alloc] initWithRootViewController:vc];
                                                    navController.navigationBar.barTintColor = [UIColor colorWithRed:0.945 green:0.318 blue:0.325 alpha:1];
                                                    [topController presentViewController:navController animated:YES completion:nil];
                                                }
                                                  error:^(LISDKAPIError *apiError) {
                                                      NSLog(@"error called %@", apiError.description);
                                                  }];
        }
    }
}

@end
