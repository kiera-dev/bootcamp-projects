from django.urls import path     
from . import views
urlpatterns = [
    path('', views.index),
    path('submit_guess/', views.submit_guess),
    path('play_again/', views.play_again),
    path('win/', views.win),
    path('low/', views.low),
    path('high/', views.high),


  ]
