%test to try to plot an animation with frames

clear
clc


%dir to work

dir1 = 'D:\DoctoradoResultados\TSEPnet28C\ChaosHenon';
cd(dir1);
cd('res');

load('allrun.mat');

minimo = min(allrun{1,1}.Network{1,1}.iteratePredF);
maximo = max(allrun{1,1}.Network{1,1}.iteratePredF);

fig=figure;
set(fig,'DoubleBuffer','on');
set(gca,'xlim',[-1 31],'ylim',[minimo maximo],...
	   'NextPlot','replace','Visible','off')
mov = avifile('example1.avi', 'colormap', colormap, 'quality', 100);

x = [1:30];
y = allrun{1,1}.Network{1,1}.iteratePredF;
for j=1:29
    h =line([x(j) x(j+1)],[y(j) y(j+1)],[1 1],'Marker','.','LineStyle','-')
    
    %    plot(x(j),allrun{1,1}.Network{1,1}.iteratePredF(j));
    
    %set(h,'EraseMode','xor');
    F = getframe(gca);
    mov = addframe(mov,F);    
end
mov = close(mov);
%movie(M);

  



