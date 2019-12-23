import {
    transition,
    trigger,
    query,
    style,
    animate,
    group,
    stagger
} from '@angular/animations';
export const PageAnimation =

    trigger('routeAnimations', [
        transition('* => *', [
            query(':enter, :leave',
                style({ position: 'fixed', width: '100%' }),
                { optional: true }),
            group([
                query(':enter', [
                    style({ transform: 'translateX(-100%)' }),
                    animate('0.5s ease-in-out',
                        style({ transform: 'translateX(0%)' }))
                ], { optional: true }),
                query(':enter h1', stagger(400, [
                    style({ transform: 'translateX(-100%)', opacity: 0 }),
                    animate('1s ease-in-out',
                        style({ transform: 'translateX(0%)', opacity: 1 }))
                ]), { optional: true }),
                query(':leave section', [
                    style({ opacity: '0' })
                ], { optional: true }),
                query(':leave', [
                    style({ transform: 'translateX(0%)' }),
                    animate('0.5s ease-in-out',
                        style({ transform: 'translateX(100%)' }))
                ], { optional: true }),
            ])
        ])
    ]);
