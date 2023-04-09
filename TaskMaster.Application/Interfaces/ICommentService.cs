﻿using Issues.Manager.Application.DTOs.Comment;

namespace Issues.Manager.Application.Services;

public interface ICommentService
{
    IEnumerable<CommentResponse> Get(int issueId);
    CommentResponse Create(CreateCommentRequest commentRequest, int issueId);
    void Delete(int issueId);
    CommentResponse Update(CreateCommentRequest comment, int issueId);
}