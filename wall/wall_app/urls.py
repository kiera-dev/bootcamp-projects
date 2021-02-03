from django.urls import path, include
from django.conf.urls import url
from . import views

urlpatterns = [
    path('', views.index),
    path('register/', views.register),
    path('login/', views.login),
    path('logout/', views.logout),
    path('success/', views.success),
    path('wall/', views.wall),
    path('message/', views.message),
    path('comment/<int:msg_id>/', views.comment),
    path('del_message/<int:msg_id>/', views.del_message),
]
